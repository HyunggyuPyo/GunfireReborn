using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Realtime;

public class LobbyPanel : MonoBehaviourPunCallbacks
{
    public Button createRoomButton;
    public Button skillButton;
    public Button exitButton;

    public GameObject SkillPanel;

    void Awake()
    {
        createRoomButton.onClick.AddListener(CreateRoomButtonClick);
        skillButton.onClick.AddListener(SkillButtonClick);
        exitButton.onClick.AddListener(ExitButtonClick);
    }

    public void CreateRoomButtonClick()
    {
        if(PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.CreateRoom(
            FirebaseManager.Instance.userData.userName,
            new RoomOptions()
            {
                MaxPlayers = 4
            });
        }        
    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        print($"�� ���� ����");
        print($"���̸� => {PhotonNetwork.CurrentRoom.Name}");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        Debug.LogError($"�� ���� ���� {message}");
    }

    public void SkillButtonClick()
    {
        //todo : ��� ��ų ����
        SkillPanel.SetActive(true);
    }

    public void ExitButtonClick()
    {
        Application.Quit();
    }
}
