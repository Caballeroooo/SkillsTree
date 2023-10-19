using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Tooltip("Список доступных со старта навыков, условно взятый из сохранений")] 
    [SerializeField] private List<SkillType> _availableSkills = new List<SkillType> { SkillType.Base };
    [SerializeField] private SkillsUIPresenter _skillsUIPresenter;
    [SerializeField] private PlayerPointsPresenter _playerPointsPresenter;
    [SerializeField] private int _startPointsCount = 5;

    private SkillsUIModel _skillsUIModel;
    private PlayerPointsModel _playerPointsModel;
    private PlayerSkillsModel _playerSkillsModel;
    private SkillsSettings _skillsSettings;

    private void OnEnable()
    {
        _skillsUIPresenter.SkillButtonSelected += OnSkillUIButtonSelected;
    }

    private void Start()
    {
        _skillsSettings = SettingsProvider.Get<SkillsSettings>();
        _skillsUIModel = new SkillsUIModel(_availableSkills);
        _playerSkillsModel = new PlayerSkillsModel(_availableSkills);
        _playerPointsModel = new PlayerPointsModel();

        _skillsUIPresenter.Initialize(_skillsUIModel, _skillsSettings);
        _skillsUIPresenter.InitializeAvailableSkills(_availableSkills);
        _playerPointsPresenter.Initialize(_playerPointsModel);
        _playerPointsModel.Add(_startPointsCount);

        _skillsUIModel.Learned += OnSkillLearned;
        _skillsUIModel.Forgotten += OnSkillForgotten;
        _skillsUIModel.AllForgotten += OnAllSkillsForgotten;
    }

    private void OnDisable()
    {
        _skillsUIPresenter.SkillButtonSelected -= OnSkillUIButtonSelected;
    }

    private void OnDestroy()
    {
        _playerPointsPresenter.Uninitialize();
        _skillsUIPresenter.Uninitialize();
        _skillsUIModel.Learned -= OnSkillLearned;
        _skillsUIModel.Forgotten -= OnSkillForgotten;
        _skillsUIModel.AllForgotten -= OnAllSkillsForgotten;
    }

    private void OnSkillUIButtonSelected(SkillType skillType)
    {
        var skillCost = _skillsSettings.GetConfig(skillType).Cost;

        if (_playerSkillsModel.CheckSkillForAvailability(skillType))
        {
            _skillsUIPresenter.DisableSelectedLearnButton();
            if (_skillsUIModel.CanBeForgotten(skillType, _skillsSettings))
            {
                _skillsUIPresenter.EnableSelectedForgotButton();
            }
            else
            {
                _skillsUIPresenter.DisableSelectedForgotButton();
            }
        }
        else
        {
            _skillsUIPresenter.DisableSelectedForgotButton();
            if (_playerPointsModel.CurrentPointsCount >= skillCost)
            {
                _skillsUIPresenter.EnableSelectedLearnButton();
            }
            else
            {
                _skillsUIPresenter.DisableSelectedLearnButton();
            }
        }
    }

    private void OnSkillLearned(SkillType skillType)
    {
        var config = _skillsSettings.GetConfig(skillType);
        _playerPointsModel.Remove(config.Cost);
        _playerSkillsModel.Learn(skillType);
        _availableSkills.Add(skillType);
        foreach (var incidentNode in config.IncidentNodes)
        {
            if (!_playerSkillsModel.CheckSkillForAvailability(incidentNode))
            {
                _skillsUIPresenter.SetDefaultState(incidentNode);
            }
        }
    }

    private void OnSkillForgotten(SkillType skillType)
    {
        var config = _skillsSettings.GetConfig(skillType);
        _playerPointsModel.Add(config.Cost);
        _playerSkillsModel.Forgot(skillType);
        _availableSkills.Remove(skillType);
        foreach (var incidentNode in config.IncidentNodes)
        {
            if (_skillsUIModel.CanBeDisabled(incidentNode, _skillsSettings))
            {
                _skillsUIPresenter.SetDisableState(incidentNode);
            }
        }
    }

    private void OnAllSkillsForgotten()
    {
        _playerSkillsModel.ForgotAll();
        foreach (var skillType in _availableSkills)
        {
            _playerPointsModel.Add(_skillsSettings.GetConfig(skillType).Cost);
        }

        _availableSkills = new List<SkillType> { SkillType.Base };
        _skillsUIPresenter.InitializeAvailableSkills(_availableSkills);
    }
}