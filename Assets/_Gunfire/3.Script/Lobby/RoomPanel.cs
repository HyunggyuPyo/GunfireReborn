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
    public Button lobbyButton;
    public Toggle readyToggle;
    public TMP_Text readyButtonText;


    public Transform[] playerPoint;
    public GameObject playerEntryPrefab;

    public Button[] inviteButtons;
    public SendMessage sendPopup;
    public ReceiveMessage receivePopup;

    private Dictionary<int, bool> playersReady;
    public Dictionary<int, PlayerEntry> playerEntries = new();

    bool playerReady = false;


    private void Awake()
    {
        readyButton.onClick.AddListener(ReadyButtonClick);
        exitButton.onClick.AddListener(ExitRoomButtonClick);
        lobbyButton.onClick.AddListener(LobbyButtonClick);

        inviteButtons[0].onClick.AddListener(InviteButtonCilck);
        inviteButtons[1].onClick.AddListener(InviteButtonCilck);
        inviteButtons[2].onClick.AddListener(InviteButtonCilck);
    }

    private void Start()
    {
        FirebaseManager.Instance.onInviteMessage += receivePopup.OnReceiveMessage; // todo 이거 로비패널에도 추가?
    }

    public override void OnEnable()
    {
        base.OnEnable();

        if(PhotonNetwork.IsMasterClient)
        {
            playersReady = new Dictionary<int, bool>();
            readyButton.interactable = false;
            readyButtonText.text = "도전 시작";
        }
        else
        {
            readyButtonText.text = "준비";
        }

        PhotonNetwork.AutomaticallySyncScene = true;

        foreach (Player player in PhotonNetwork.CurrentRoom.Players.Values)
        {
            JoinPlayer(player);

            if (player.CustomProperties.ContainsKey("Ready"))
            {
                SetPlayerReady(player.ActorNumber, (bool)player.CustomProperties["Ready"]);
            }
        }

        exitButton.gameObject.SetActive(!PhotonNetwork.IsMasterClient);
    }

    public void JoinPlayer(Player newPlayer)
    {
        var playerEntry = Instantiate(playerEntryPrefab, playerPoint[newPlayer.ActorNumber - 1], false).GetComponent<PlayerEntry>(); 

        playerEntry.player = newPlayer;
        playerEntry.nickNameText.text = newPlayer.NickName;

        if(PhotonNetwork.LocalPlayer.ActorNumber == newPlayer.ActorNumber)
        {
            //readyButton.onClick.AddListener(ReadyButtonClick);
        }

        playerEntries[newPlayer.ActorNumber] = playerEntry;

        if(PhotonNetwork.IsMasterClient)
        {
            playersReady[newPlayer.ActorNumber] = false;

            if(PhotonNetwork.LocalPlayer.ActorNumber == newPlayer.ActorNumber)
            {
                playerEntries[newPlayer.ActorNumber].readyText.text = "대장";
                playersReady[newPlayer.ActorNumber] = true;
            }

            CheckReady();
        }

        SortPlayers();
    }

    public void SortPlayers()
    {
        List<int> keysToRemave = new List<int>();

        foreach (int actorNumber in playerEntries.Keys)
        {
            if(playerEntries[actorNumber] == null)
            {
                keysToRemave.Add(actorNumber);
            }
        }

        foreach (int key in keysToRemave)
        {
            playerEntries.Remove(key);
        }

        foreach (int actorNumber in playerEntries.Keys)
        {
            playerEntries[actorNumber].transform.SetSiblingIndex(actorNumber);
        }
    }

    public override void OnDisable()
    {
        base.OnDisable();

        for (int i = 0; i < playerPoint.Length; i++)
        {
            if (playerPoint[i] != null && playerPoint[i].childCount != 0)
                Destroy(playerPoint[i].GetChild(0).gameObject);
        }
        //todo 예외 처리
    }

    private void CheckReady()
    {
        bool allReady = playersReady.Values.All(x => x);

        if(PhotonNetwork.IsMasterClient)
        {
            readyButton.interactable = allReady;
        }
    }
    
    public void SetPlayerReady(int actorNumber, bool isReady)
    {
        playerEntries[actorNumber].ReadyTextChange(isReady);

        if (PhotonNetwork.IsMasterClient)
        {
            playersReady[actorNumber] = isReady;
            CheckReady();
        }
    } //todo : actornumber 1인 놈을 일단 대장으로 띄우고 만약 나가면 다른 사람이 방장 될때 int 변수에 번호 저장하게 해서 그놈을 대장으로 띄우기 

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print($"방에 들어왔어!");
    }

    public void LeavePlayer(Player gonePlayer)
    {
        GameObject leaveTarget = playerEntries[gonePlayer.ActorNumber].gameObject;
        // 그냥 leaveTarget으로 바로 destory
        playerEntries.Remove(gonePlayer.ActorNumber);
        Destroy(playerPoint[gonePlayer.ActorNumber - 1].GetChild(0).gameObject);

        if (PhotonNetwork.IsMasterClient)
        {
            playersReady.Remove(gonePlayer.ActorNumber);
            CheckReady();
        }

        SortPlayers();
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, PhotonHashtable changedProps)
    {
        if(changedProps.ContainsKey("Ready"))
        {
            SetPlayerReady(targetPlayer.ActorNumber, (bool)changedProps["Ready"]);
        }
    }

    void ReadyButtonClick()
    {
        if(!PhotonNetwork.IsMasterClient) 
        {
            if(PhotonNetwork.LocalPlayer.IsLocal)
            {
                Player localPlayer = PhotonNetwork.LocalPlayer;
                PhotonHashtable customProps = localPlayer.CustomProperties;

                if (false == playerReady)
                {
                    playerReady = true;
                }
                else
                {
                    playerReady = false;
                }

                customProps["Ready"] = playerReady;
                localPlayer.SetCustomProperties(customProps);

                playerEntries[localPlayer.ActorNumber].GetComponent<PlayerEntry>().ReadyTextChange(playerReady);
            }
        }
        else
        {
            PhotonNetwork.LoadLevel("Game"); // todo "Game"으로 바꾸기 
        }
    }

    void ExitRoomButtonClick()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.AutomaticallySyncScene = false;
        //OnConnectedToMaster();
    }

    void InviteButtonCilck()
    {
        sendPopup.gameObject.SetActive(true);
    }

    void LobbyButtonClick()
    {
        PhotonNetwork.LeaveRoom();
    }

}
