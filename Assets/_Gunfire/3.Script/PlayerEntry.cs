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
        if(!on)
        {
            readyText.color = Color.red;
        }
        else
        {
            readyText.color = Color.green;
        }
    }

}
