using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxCharacter : Character
{
    public override void InitSetting()
    {
        characterData.maxHealth = 65;
        characterData.maxShield = 65;
        characterData.speed = 6f;
    }

    public override void MainSkill()
    {
        print("���߿� �߰��� ��ų");
    }

    public override void SubSkill()
    {
        print("���߿� �߰��� ��ų");
    }
}
