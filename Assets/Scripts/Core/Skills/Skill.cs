public class Skill
{
    private SkillType _type;

    public SkillType Type => _type;

    public Skill(SkillType type)
    {
        _type = type;
    }
}
