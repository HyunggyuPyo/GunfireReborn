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
        msgText.text = $"{inviter}님으로부터 초대받으셨습니다. \n 파티에 참가하시겠습니까?";
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
        print("방을 나왔습니다");

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
        print("마스터 서버 접속");
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