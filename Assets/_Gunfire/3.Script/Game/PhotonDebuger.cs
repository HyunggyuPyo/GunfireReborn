using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;

public class PhotonDebuger : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        PhotonNetwork.NickName = $"Tester {Random.Range(1, 10)}";
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        RoomOptions testroomOptions = new RoomOptions()
        {
            IsVisible = false,
            MaxPlayers = 4
        };
        PhotonNetwork.JoinOrCreateRoom("TestRoom", testroomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        GameStartManager.debugReady = true;
    }

}
