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

/*
  재능스킬 페이지 
 끝나고 돈이랑 영혼석 획득 화면 (게임 정산화면)
 게임 오버 없음
 플레이어 실드 회복도 없음
 넥네임 중복 검사
 검은 화면으로 시작하고 캐릭터 생성되면 콜백으로 화면 내리기(로딩창 처럼 보이기)
 */