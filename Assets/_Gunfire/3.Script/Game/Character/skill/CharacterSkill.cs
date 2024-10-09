using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class CharacterSkill : MonoBehaviour
{
    public CharacterSkillSO data;
    int mainCount;
    public int subCount { get; set; }
    bool mainskill = true;

    private void Awake()
    {
        mainCount = data.mainSkillCount;
        subCount = data.subSkillCount;
        PlayerWeaponUI.Instance.SetSkillPoint(subCount);
    }

    public abstract void MainSkill();

    public abstract void SubSkill();

    public virtual void UseSkill()
    {
        if(Input.GetKeyDown(KeyCode.E) && mainskill)//&&  mainCount != 0)
        {
            MainSkill();
            mainskill = false;
            //mainCount -= 1;

            StartCoroutine(Dealy());
            StartCoroutine(PlayerWeaponUI.Instance.MainSkillDelay(data.mainSkillCoolTime));
        }

        if(Input.GetKeyDown(KeyCode.Q) && subCount != 0)
        {
            SubSkill();
            subCount -= 1;
            PlayerWeaponUI.Instance.SetSkillPoint(subCount);
        }
    }

    public void GutSubSkillPoint()
    {
        subCount += 1;
        PlayerWeaponUI.Instance.SetSkillPoint(subCount);
    }

    IEnumerator Dealy()
    {
        yield return new WaitForSeconds(data.mainSkillCoolTime);
        mainskill = true;
    }
}
