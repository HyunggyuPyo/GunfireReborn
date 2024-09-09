using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour, IHitable
{
    public Character playerCharacter;
    CharacterData data;

    private void Awake()
    {
        playerCharacter.InitSetting();
        data = playerCharacter.characterData;
        // todo : ���߿� ������� ���� ���� json���� �ҷ��ͼ� data���� �����ֱ�
        PlayerInfoUI.Instance.InitUI(data.maxShield, data.maxHealth);
    }

    private void Start()
    {
        
    }

    public void Hit(int damage)
    {
        if(data.shield < damage)
        {
            if(data.shield != 0)
            {
                data.hp = damage - data.shield;
                data.shield = 0;
            }
            else
            {
                data.hp -= damage;
            }
        }
        else
        {
            data.shield -= damage; 
        }
    }
}
