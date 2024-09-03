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
        print("ReadyTextChange함수 실행 됨");
        if(false == on)
        {
            readyText.color = Color.red;
            readyText.text = "미준비";
        }
        else
        {
            readyText.color = Color.green;
            readyText.text = "준비 완료";
        }
    }

}
