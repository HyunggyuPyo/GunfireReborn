using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PhotonHashtable = ExitGames.Client.Photon.Hashtable;
using System.Linq;

public class RoomPanel : MonoBehaviourPunCallbacks
{
    public Button readyButton;
    public Button exitButton;
    public Toggle readyToggle;
    public TMP_Text readyButtonText;

    //public Transform entryPlayers;
    public Transform[] playerPoint;
    public GameObject playerEntryPrefab;
    public Button[] InviteButtons;

    private Dictionary<int, bool> playersReady;
    public Dictionary<int, PlayerEntry> playerEntries = new();

    bool playerReady = false;


    private void Awake()
    {
        readyButton.onClick.AddListener(ReadyButtonClick);
        exitButton.onClick.AddListener(ExitRoomButtonClick);
        
    }

    public override void OnEnable()
    {
        base.OnEnable();

        if(PhotonNetwork.IsMasterClient)
        {
            playersReady = new Dictionary<int, bool>();
            readyButton.interactable = false;
        }

        PhotonNetwork.AutomaticallySyncScene = true;

        //foreach (Player player in PhotonNetwork.CurrentRoom.Players.Values)
        //{
        //    JoinPlayer(player);

        //    if(player.CustomProperties.ContainsKey("Ready"))
        //    {
        //        SetPlayerReady(player.ActorNumber, (bool)player.CustomProperties["Ready"]);
        //    }
        //}
    }

    private void CheckReady()
    {
        bool allReady = playersReady.Values.All(x => x);

        if(PhotonNetwork.IsMasterClient)
        {
            readyButton.interactable = true;
        }
    }

    //public void JoinPlayer(Player newPlayer)
    //{
    //    var playerEntry = Instantiate(playerEntryPrefab, entryPlayers, false).GetComponent<PlayerEntry>();

    //    playerEntry.player = newPlayer;
    //    playerEntry.nickNameText.text = newPlayer.NickName;

    //    playerEntries[newPlayer.ActorNumber] = playerEntry;

    //    if(PhotonNetwork.IsMasterClient)
    //    {
    //        playersReady[newPlayer.ActorNumber] = false;
    //        CheckReady();
    //    }

    //    SortPlayers();
    //}

    public void SetPlayerReady(int actorNumber, bool isReady)
    {
        playerEntries[actorNumber].ReadyTextChange(playerReady);

        if(PhotonNetwork.IsMasterClient)
        {
            playersReady[actorNumber] = isReady;
            CheckReady();
        }
    }

    //void SortPlayers()
    //{
    //    foreach (int actorNumber in playerEntries.Keys)
    //    {
    //        playerEntries[actorNumber].transform.SetSiblingIndex(actorNumber);
    //    }
    //}

    public override void OnDisable()
    {
        base.OnDisable();

        for (int i = 0; i < playerPoint.Length; i++)
        {
            Destroy(playerPoint[i].GetChild(0).gameObject);
        }
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        PhotonNetwork.CreateRoom(
            FirebaseManager.Instance.userData.userName,
            new RoomOptions()
            {
                MaxPlayers = 4
            });

        print(FirebaseManager.Instance.userData.userName);
        readyButtonText.text = "게임 시작";

        //todo :  이부분 OnJoinedRoom()으로 내리는게 맞을까?
        Instantiate(playerEntryPrefab, playerPoint[0], false);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        if(!PhotonNetwork.IsMasterClient)
        {
            readyButtonText.text = "준비";
            Instantiate(playerEntryPrefab, playerPoint[PhotonNetwork.CurrentRoom.PlayerCount - 1], false);
        }   
    }

    void ReadyButtonClick()
    {
        if(PhotonNetwork.IsMasterClient) 
        {
            //if(모두가 레디 상태일때)
            PhotonNetwork.LoadLevel("Game");
        }
        //else
        //{
        //    if (playerReady)
        //        playerReady = false;
        //    else
        //        playerReady = true;

        //    Player localPlayer = PhotonNetwork.LocalPlayer;
        //    PhotonHashtable customProps = localPlayer.CustomProperties;

        //    customProps["Ready"] = playerReady;
        //    localPlayer.SetCustomProperties(customProps);
        //}
    }

    void ExitRoomButtonClick()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.AutomaticallySyncScene = false;
        OnConnectedToMaster();
    }

}
