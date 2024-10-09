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
            PlayerWeaponManager.Instance.Guns.Clear();
            PlayerWeaponManager.Instance.Guns.AddRange(new GameObject[] {guns[0], guns[1], guns[2]});
            for (int i = 0; i < guns.Count; i++)
            {
                if (guns[i] != null)
                {
                    guns[i].gameObject.transform.SetParent(PlayerWeaponManager.Instance.weapons.transform);
                    guns[i].GetComponent<Gun>().
                }                  
            }
            Inventory.Instance.coin = this.coin;
            Inventory.Instance.soulStone = this.soulstone;
            FoxSkill.instance.subCount = this.subSkillCount;
            loadData = false;
        }        
    }
}
