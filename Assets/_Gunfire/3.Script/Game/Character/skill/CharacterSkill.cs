using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterSkill : MonoBehaviour
{
    public CharacterSkillSO data;
    int mainDelay;
    int mainCount;
    int subCount;

    private void Awake()
    {
        mainDelay = data.mainSkillCoolTime;
        mainCount = data.mainSkillCount;
        subCount = data.subSkillCount;
    }

    public abstract void MainSkill();

    public abstract void SubSkill();

    public virtual void UseSkill()
    {
        if(Input.GetKeyDown(KeyCode.E) && mainDelay != 0 && mainCount != 0)
        {
            MainSkill();
            mainCount -= 1;
            // uiÄÄÆ÷³ÍÆ®¶û ¿¬µ¿ÇØ¼­ µô·¹ÀÌ µ¹¸®±â
            print("mainskill");
        }

        if(Input.GetKeyDown(KeyCode.Q) && subCount != 0)
        {
            SubSkill();
            subCount -= 1;
            print("subskill");
        }
    }
}
