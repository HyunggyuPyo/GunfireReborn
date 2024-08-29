using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReceiveMessage : MonoBehaviour
{
    public Text msgText;
    public Button refusalButton; // 거절
    public Button acceptanceButton; //수락

    private string roomName;

    private void Awake()
    {
        refusalButton.onClick.AddListener(RefusalButtonClick);
        acceptanceButton.onClick.AddListener(AcceptanceButtonClick);
    }

    public void OnReceiveMessage(string msg)
    {
        msgText.text = $"{msg}님으로부터 초대받으셨습니다. \n 파티에 참가하시겠습니까?";
        roomName = msg;
        gameObject.SetActive(true);
    }

    public void RefusalButtonClick()
    {
        gameObject.SetActive(false);
    }

    public void AcceptanceButtonClick()
    {
        PhotonNetwork.JoinRoom(roomName);
        gameObject.SetActive(false);
    }
}
