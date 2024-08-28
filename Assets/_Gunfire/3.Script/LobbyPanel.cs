using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyPanel : MonoBehaviourPunCallbacks
{
    public Button readyButton;
    public Button exitButton;
    public Toggle readyToggle;

    public Transform[] playerPoint;
    public GameObject playerEntryPrefab;

    private Dictionary<int, bool> playersReady;
    public Dictionary<int, PlayerEntry> playerEntries = new();


    private void Awake()
    {
        readyButton.onClick.AddListener(ReadyButtonClick);
        exitButton.onClick.AddListener(ExitRoomButtonClick);
    }

    public override void OnEnable()
    {
        base.OnEnable();

        PhotonNetwork.CreateRoom(
            FirebaseManager.Instance.userData.userName,
            new RoomOptions()
            {
                MaxPlayers = 4
            });

        print(FirebaseManager.Instance.userData.userName);

        Instantiate(playerEntryPrefab, playerPoint[0], false);
    }

    public override void OnDisable()
    {
        base.OnDisable();

        for (int i = 0; i < playerPoint.Length; i++)
        {
            Destroy(playerPoint[i].GetChild(0).gameObject);
        }
    }

    void ReadyButtonClick()
    {

    }

    void ExitRoomButtonClick()
    {

    }

}
