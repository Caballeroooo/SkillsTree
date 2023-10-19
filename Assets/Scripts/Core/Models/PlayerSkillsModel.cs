using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerSkillsModel
{
    private List<Skill> _learnedSkills;

    public PlayerSkillsModel(List<SkillType> availableSkills)
    {
        _learnedSkills = new List<Skill>();
        foreach (var skillType in availableSkills)
        {
            _learnedSkills.Add(new Skill(skillType));
        }
    }

    public void Learn(SkillType skillType)
    {
        if (!CheckSkillForAvailability(skillType))
        {
            _learnedSkills.Add(new Skill(skillType));
        }
        else
        {
            Debug.LogWarning($"Skill \"{skillType}\" already learned");
        }
    }

    public void Forgot(SkillType skillType)
    {
        if (CheckSkillForAvailability(skillType))
        {
            var skill = _learnedSkills.First(skill => skill.Type == skillType);
            _learnedSkills.Remove(skill);
        }
        else
        {
            Debug.LogWarning($"Skill \"{skillType}\" already forgotten");
        }
    }

    public void ForgotAll()
    {
        _learnedSkills = new List<Skill>();
        _learnedSkills.Add(new Skill(SkillType.Base));
    }

    public bool CheckSkillForAvailability(SkillType skillType)
    {
        var skill = _learnedSkills.FirstOrDefault(skill => skill.Type == skillType);
        return _learnedSkills.Contains(skill);
    }
}
