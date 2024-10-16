using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager instance;
    public bool loadData { get; set; }

    public int hp { get; set; }
    public int shield { get; set; }
    public List<GameObject> guns { get; set; }
    public int coin { get; set; }
    public int soulstone { get; set; }
    public int subSkillCount { get; set; }

    public int upgradeCount { get; set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }

        loadData = false;
        upgradeCount = 3;
    }

    public void PlayerGetSkillData()
    {
        foreach (KeyValuePair<SkillType, int> entry in FirebaseManager.Instance.skillData)
        {
            if (entry.Value != 0)
            {
                SkillData(entry.Key, entry.Value);
            }
        }
    }

    void SkillData(SkillType type, int level)
    {
        switch (type)
        {
            case SkillType.skill_quest_01:
                Inventory.Instance.coin += level * 100;
                break;
            case SkillType.skill_quest_02:
                Inventory.Instance.bonusCoin = level * 5;
                break;
            case SkillType.skill_battle_01:
                PlayerWeaponManager.Instance.bonusDamage = level * 5;
                break;
            case SkillType.skill_battle_02:
                upgradeCount += level;
                break;
            case SkillType.skill_battle_03:
                // 럭키샷 증가
                break;
            case SkillType.skill_live_01:
                hp += level * 5;
                break;
            case SkillType.skill_live_02:
                shield += level * 5;
                break;
            case SkillType.skill_live_03:
                //방어력 증가
                break;
        }
    }

    public void SaveData()
    {
        this.hp = CharacterManager.instance.hp;
        this.shield = CharacterManager.instance.shield;
        this.guns = PlayerWeaponManager.Instance.Guns;
        print($"{guns[0]}/{guns[1]}/{guns[2]}");
        for (int i = 0; i < guns.Count; i++)
        {
            if(guns[i] != null)
                guns[i].gameObject.transform.SetParent(transform);
        }
        this.coin = Inventory.Instance.coin;
        this.soulstone = Inventory.Instance.soulStone;
        this.subSkillCount = FoxSkill.instance.subCount;
        loadData = true;
    }

    public void LoadData()
    {
        if(loadData)
        {
            CharacterManager.instance.hp = this.hp;
            CharacterManager.instance.shield = this.shield;
            PlayerWeaponManager.Instance.ResetWeapon();
            PlayerWeaponManager.Instance.Guns.Clear();
            PlayerWeaponManager.Instance.Guns.AddRange(new GameObject[] {guns[0], guns[1], guns[2]});
            for (int i = 0; i < guns.Count; i++)
            {
                if (guns[i] != null)
                {
                    guns[i].gameObject.transform.SetParent(PlayerWeaponManager.Instance.weapons.transform);
                    guns[i].gameObject.transform.localPosition = Vector3.zero;
                    guns[i].gameObject.transform.localRotation = Quaternion.identity;
                    guns[i].gameObject.transform.localScale = Vector3.one;
                }                  
            }
            Inventory.Instance.coin = this.coin;
            Inventory.Instance.soulStone = this.soulstone;
            FoxSkill.instance.subCount = this.subSkillCount;
            loadData = false;
        }        
    }
}
