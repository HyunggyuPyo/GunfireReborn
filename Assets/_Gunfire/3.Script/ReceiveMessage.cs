using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReceiveMessage : MonoBehaviour
{
    public Text msgText;
    public Button refusalButton; // ����
    public Button acceptanceButton; //����

    private string roomName;

    private void Awake()
    {
        refusalButton.onClick.AddListener(RefusalButtonClick);
        acceptanceButton.onClick.AddListener(AcceptanceButtonClick);
    }

    public void OnReceiveMessage(string msg)
    {
        msgText.text = $"{msg}�����κ��� �ʴ�����̽��ϴ�. \n ��Ƽ�� �����Ͻðڽ��ϱ�?";
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
