using System;
using UnityEngine;

[Serializable]
public struct SkillConfig
{
    [SerializeField] private SkillType _type;
    [SerializeField] private string _name;
    [SerializeField] private int _cost;
    [SerializeField] private SkillType[] _incidentNodes;

    public SkillType Type => _type;
    public string Name => _name;
    public int Cost => _cost; 
    public SkillType[] IncidentNodes => _incidentNodes;
}
