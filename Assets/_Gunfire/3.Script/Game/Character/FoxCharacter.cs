using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxCharacter : Character
{
    public override void InitSetting()
    {
        characterData.maxHealth = 65;
        characterData.hp = 65;
        characterData.maxShield = 65;
        characterData.shield = 65;
        characterData.speed = 7f;
    }

    public override void MainSkill()
    {
        print("나중에 추가할 스킬");
    }

    public override void SubSkill()
    {
        print("나중에 추가할 스킬");
    }
}
