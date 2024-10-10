using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSkill", menuName = "SO/Add CharacterSkill")]
public class CharacterSkillSO : ScriptableObject
{
    public GameObject mainSkillPrefab;
    public int mainSkillDamage;
    public int mainSkillCount;
    public int mainSkillCoolTime;
    public GameObject mainSkillImage;
    public AudioClip mainSkillClip;

    public GameObject subSkillPrefab;
    public int subSkillDamage;
    public int subSkillCount;
    public Sprite subSkillImage;
    public AudioClip subSkillClip;
}
