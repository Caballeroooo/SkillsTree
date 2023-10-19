using System;
using System.Collections.Generic;

public class SkillsUIModel
{
    private List<SkillType> _learnedSkills;
    private SkillType _lastSelectedType;
    private int _clicksOnSkillButtonCount;

    public event Action SkillSelectedStateSwitched;
    public event Action<SkillType> Learned;
    public event Action<SkillType> Forgotten;
    public event Action AllForgotten;

    public bool IsSkillSelected => _clicksOnSkillButtonCount % 2 == 1;

    public SkillsUIModel(List<SkillType> availableSkills)
    {
        _learnedSkills = new List<SkillType>();
        foreach (var skillType in availableSkills)
        {
            _learnedSkills.Add(skillType);
        }
    }

    public void SwitchSelectedState(SkillType skillType)
    {
        _clicksOnSkillButtonCount++;
        _lastSelectedType = skillType;
        SkillSelectedStateSwitched?.Invoke();
    }

    public void LearnSkill()
    {
        _learnedSkills.Add(_lastSelectedType);
        Learned?.Invoke(_lastSelectedType);
    }

    public void ForgotSkill()
    {
        _learnedSkills.Remove(_lastSelectedType);
        Forgotten?.Invoke(_lastSelectedType);
    }

    public void ForgotAll()
    {
        _learnedSkills = new List<SkillType> { SkillType.Base };
        AllForgotten?.Invoke();
    }

    public bool CanBeDisabled(SkillType type, SkillsSettings settings)
    {
        foreach (var skillType in _learnedSkills)
        {
            var config = settings.GetConfig(skillType);
            foreach (var incidentNode in config.IncidentNodes)
            {
                if (incidentNode == type)
                    return false;
            }
        }

        return true;
    }

    public bool CanBeForgotten(SkillType type, SkillsSettings settings)
    {
        var currentTypeConfig = settings.GetConfig(type);

        foreach (var incidentNode in currentTypeConfig.IncidentNodes)
        {
            if (CheckSkillForAvailability(incidentNode))
            {
                if (!CheckConnectionWithBaseSkill(incidentNode, type, settings))
                    return false;
            }
        }

        return true;
    }

    private bool CheckConnectionWithBaseSkill(SkillType type, SkillType excludeType, SkillsSettings settings)
    {
        var queue = new Queue<SkillType>();
        var visited = new HashSet<SkillType>();
        queue.Enqueue(type);

        while (queue.Count != 0)
        {
            var currentType = queue.Dequeue();

            if (visited.Contains(currentType))
                continue;
            
            if (CheckSkillForAvailability(currentType) == false || currentType == excludeType)
                continue;

            if (currentType == SkillType.Base)
                return true;
            
            visited.Add(currentType);
            var nextTypeConfig = settings.GetConfig(currentType);
            foreach (var nextNode in nextTypeConfig.IncidentNodes)
            {
                queue.Enqueue(nextNode);
            }
        }
        
        return false;
    }

    private bool CheckSkillForAvailability(SkillType skillType)
    {
        return _learnedSkills.Contains(skillType);
    }
}