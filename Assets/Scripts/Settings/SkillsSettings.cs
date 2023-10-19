using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillsSettings", menuName = "Settings/SkillsSettings", order = 0)]
public class SkillsSettings : ScriptableObject
{
    [SerializeField] private SkillConfig[] _configs;

    public SkillConfig GetConfig(SkillType type)
    {
        return _configs.First(config => config.Type == type);
    }
}
