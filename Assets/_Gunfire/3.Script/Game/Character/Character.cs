using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CharacterData
{
    public int maxHealth;
    public int maxShield;
    public float speed;
}

public abstract class Character : MonoBehaviour
{
    public CharacterData characterData;

    public abstract void InitSetting();

    public abstract void SubSkill();

    public abstract void MainSkill();
}
