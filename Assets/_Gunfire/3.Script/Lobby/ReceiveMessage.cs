using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReceiveMessage : MonoBehaviourPunCallbacks
{
    public TMP_Text msgText;
    public Button refusalButton;
    public Button acceptanceButton;

    private string roomName;

    private void Awake()
    {
        refusalButton.onClick.AddListener(RefusalButtonClick);
        acceptanceButton.onClick.AddListener(AcceptanceButtonClick);
    }

    public void OnReceiveMessage(string inviter)
    {
        msgText.text = $"{inviter}�����κ��� �ʴ�����̽��ϴ�. \n ��Ƽ�� �����Ͻðڽ��ϱ�?";
        roomName = inviter;
        gameObject.SetActive(true);
    }

    public void RefusalButtonClick()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
        else
        {
            JoinTargetRoom();
        }
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        print("���� ���Խ��ϴ�");

        PhotonNetwork.ConnectUsingSettings();
    }

    void JoinTargetRoom()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRoom(roomName);
        }
        else
        {
            Debug.LogWarning("Not connected to the server.");
        }
    }
    public override void OnConnectedToMaster()
    {
        print("������ ���� ����");
        PhotonNetwork.JoinRoom(roomName);
    }

    public void AcceptanceButtonClick()
    {
        gameObject.SetActive(false);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        gameObject.SetActive(false);
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        JoinTargetRoom();
    }

}