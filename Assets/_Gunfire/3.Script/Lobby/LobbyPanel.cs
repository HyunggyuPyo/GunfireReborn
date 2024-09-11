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

    void Awake()
    {
        createRoomButton.onClick.AddListener(CreateRoomButtonClick);
        skillButton.onClick.AddListener(SkillButtonClick);
        exitButton.onClick.AddListener(ExitButtonClick);
    }

    public void CreateRoomButtonClick()
    {
        PhotonNetwork.CreateRoom(
            FirebaseManager.Instance.userData.userName,
            new RoomOptions()
            {
                MaxPlayers = 4
            });
    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        print($"방 생성 성공");
        print($"방이름 => {PhotonNetwork.CurrentRoom.Name}");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        Debug.LogError($"방 생성 실패 {message}");
    }

    public void SkillButtonClick()
    {
        //todo : 재능 스킬 제작
    }

    public void ExitButtonClick()
    {
        //todo : 게임종료
    }
}
