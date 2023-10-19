using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillsUIPresenter : MonoBehaviour
{
    [SerializeField] private BaseSkillUIView[] _skillUIViews;
    [SerializeField] private DefaultButtonView _forgotAllButton;
    [SerializeField] private UIBackgroundFade _uiBackgroundFade;

    private SkillsUIModel _skillsUIModel;
    private BaseSkillUIView _lastSelectedSkillUIView;
    private SkillsSettings _skillsSettings;

    public event Action<SkillType> SkillButtonSelected;

    public void Initialize(SkillsUIModel skillsUIModel, SkillsSettings settings)
    {
        _skillsUIModel = skillsUIModel;
        _skillsSettings = settings;
        _skillsUIModel.SkillSelectedStateSwitched += OnSkillUISelectedStateSwitched;
        _forgotAllButton.Clicked += OnForgotAllClicked;
        _uiBackgroundFade.Clicked += OnUIBackgroundFadeClicked;
        
        InitializeSkillsViews();
    }

    public void Uninitialize()
    {
        _skillsUIModel.SkillSelectedStateSwitched -= OnSkillUISelectedStateSwitched;
        _forgotAllButton.Clicked -= OnForgotAllClicked;
        _uiBackgroundFade.Clicked -= OnUIBackgroundFadeClicked;
        UninitializeSkillsViews();
        _skillsUIModel = null;
    }

    public void InitializeAvailableSkills(List<SkillType> availableSkills)
    {
        DisableAllSkillsViews();
        foreach (var skillUIView in availableSkills.Select(GetSkillUIView))
        {
            skillUIView.SetLearnedState();
            var config = _skillsSettings.GetConfig(skillUIView.Type);
            foreach (var incidentNode in config.IncidentNodes)
            {
                if (availableSkills.Contains(incidentNode))
                    continue;
                
                var nextSkillUIView = GetSkillUIView(incidentNode);
                nextSkillUIView.SetDefaultState();
            }
        }
    }

    public void EnableSelectedLearnButton()
    {
        _lastSelectedSkillUIView.EnableLearnButton();
    }

    public void DisableSelectedLearnButton()
    {
        _lastSelectedSkillUIView.DisableLearnButton();
    }

    public void EnableSelectedForgotButton()
    {
        _lastSelectedSkillUIView.EnableForgotButton();
    }

    public void DisableSelectedForgotButton()
    {
        _lastSelectedSkillUIView.DisableForgotButton();
    }

    public void SetDefaultState(SkillType type)
    {
        var skillUIView = GetSkillUIView(type);
        skillUIView.SetDefaultState();
    }

    public void SetDisableState(SkillType type)
    {
        var skillUIView = GetSkillUIView(type);
        skillUIView.SetDisableState();
    }

    private void InitializeSkillsViews()
    {
        for (var i = 0; i < _skillUIViews.Length; i++)
        {
            _skillUIViews[i].Initialize(_skillsSettings.GetConfig(_skillUIViews[i].Type));
            _skillUIViews[i].HideDescription();
            _skillUIViews[i].Clicked += OnSkillButtonClicked;
            _skillUIViews[i].LearnButtonClicked += OnLearnButtonClicked;
            _skillUIViews[i].ForgotButtonClicked += OnForgotButtonClicked;
            if (i != 0)
            {
                _skillUIViews[i].SetTitle(i.ToString());
            }
        }
    }

    private void UninitializeSkillsViews()
    {
        foreach (var skillUIView in _skillUIViews)
        {
            skillUIView.Clicked -= OnSkillButtonClicked;
            skillUIView.LearnButtonClicked -= OnLearnButtonClicked;
            skillUIView.ForgotButtonClicked -= OnForgotButtonClicked;
        }
    }

    private void DisableAllSkillsViews()
    {
        foreach (var skillUIView in _skillUIViews)
        {
            skillUIView.SetDisableState();
        }
    }

    private void OnSkillButtonClicked(SkillType skillType)
    {
        _lastSelectedSkillUIView = GetSkillUIView(skillType);
        _skillsUIModel.SwitchSelectedState(skillType);
    }

    private void OnUIBackgroundFadeClicked()
    {
        _skillsUIModel.SwitchSelectedState(_lastSelectedSkillUIView.Type);
    }

    private void OnSkillUISelectedStateSwitched()
    {
        if (_skillsUIModel.IsSkillSelected)
        {
            _uiBackgroundFade.transform.SetSiblingIndex(transform.childCount - 1);
            _lastSelectedSkillUIView.transform.SetSiblingIndex(transform.childCount);
            _lastSelectedSkillUIView.ShowDescription();
            _uiBackgroundFade.Show();

            SkillButtonSelected?.Invoke(_lastSelectedSkillUIView.Type);
        }
        else
        {
            _lastSelectedSkillUIView.HideDescription();
            _uiBackgroundFade.Hide();
        }
    }

    private void OnLearnButtonClicked()
    {
        _skillsUIModel.LearnSkill();
        _lastSelectedSkillUIView.DisableLearnButton();
        _lastSelectedSkillUIView.EnableForgotButton();
        _lastSelectedSkillUIView.SetLearnedState();
    }

    private void OnForgotButtonClicked()
    {
        _skillsUIModel.ForgotSkill();
        _lastSelectedSkillUIView.EnableLearnButton();
        _lastSelectedSkillUIView.DisableForgotButton();
        _lastSelectedSkillUIView.SetDefaultState();
    }

    private void OnForgotAllClicked()
    {
        _skillsUIModel.ForgotAll();
    }

    private BaseSkillUIView GetSkillUIView(SkillType type)
    {
        return _skillUIViews.First(button => button.Type == type);
    }
}