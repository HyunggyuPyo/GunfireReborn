using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerEntry : MonoBehaviour
{
    public TMP_Text nickNameText;
    public TMP_Text readyText;

    public Player player;

    public void ReadyTextChange(bool on)
    {
        print("ReadyTextChange�Լ� ���� ��");
        if(false == on)
        {
            readyText.color = Color.red;
            readyText.text = "���غ�";
        }
        else
        {
            readyText.color = Color.green;
            readyText.text = "�غ� �Ϸ�";
        }
    }

}
