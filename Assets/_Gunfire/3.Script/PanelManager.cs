using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PanelManager : MonoBehaviourPunCallbacks
{
    public static PanelManager Instance { get; private set; }

    public LoginPanel login;
    public LobbyPanel lobby;
    public RoomPanel room;

    private Dictionary<string, GameObject> PanelTable;

    void Awake()
    {
        Instance = this;
        PanelTable = new Dictionary<string, GameObject>
        {
            { "Login", login.gameObject },
            { "Lobby", lobby.gameObject },
            { "Room", room.gameObject }
        };

        PanelOpen("Login");
        PhotonNetwork.AddCallbackTarget(this);
    }

    public void PanelOpen(string panelName)
    {
        foreach (var row in PanelTable)
        {
            row.Value.SetActive(row.Key == panelName);
        }
    }

    public override void OnConnected() //OnJoinedLobby
    {
        PanelOpen("Lobby");
    }

    public override void OnJoinedRoom()
    {
        PanelOpen("Room");
    }

    public override void OnCreatedRoom()
    {
        PanelOpen("Room");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        PanelOpen("Login");
    }
}
