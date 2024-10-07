using UnityEngine;

public enum SkillType
{
    skill_quest_01,
    skill_quest_02,
    skill_battle_01,
    skill_battle_02,
    skill_battle_03,
    skill_live_01,
    skill_live_02,
    skill_live_03,
}

public enum SkillClass
{
    quest,
    battle,
    live
}

[CreateAssetMenu(fileName = "Skill", menuName = "SO/Add Skill")]
public class SkillSO : ScriptableObject
{
    public SkillType type;
    public SkillClass skillClass;
    public int maxLevel;
    public string skillName;
    public string tooltip;
    public int price;
    public Sprite image;
    public string[] levelTooltip;
}