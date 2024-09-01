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

    //public Transform entryPlayers;
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
        }

        PhotonNetwork.AutomaticallySyncScene = true;

        foreach (Player player in PhotonNetwork.CurrentRoom.Players.Values)
        {
            //플레이어 다른방 입장 만들어야 됨
            //JoinPlayer(player); 오류 수정

            if (player.CustomProperties.ContainsKey("Ready"))
            {
                SetPlayerReady(player.ActorNumber, (bool)player.CustomProperties["Ready"]);
            }
        }

    }

    private void CheckReady()
    {
        bool allReady = playersReady.Values.All(x => x);

        if(PhotonNetwork.IsMasterClient)
        {
            readyButton.interactable = true;
        }
    }


    public void SetPlayerReady(int actorNumber, bool isReady)
    {
        playerEntries[actorNumber].ReadyTextChange(playerReady);

        if(PhotonNetwork.IsMasterClient)
        {
            playersReady[actorNumber] = isReady;
            CheckReady();
        }
    }

    public override void OnDisable()
    {
        base.OnDisable();

        for (int i = 0; i < playerPoint.Length; i++)
        {
            if(playerPoint[i].childCount != 0)
                Destroy(playerPoint[i].GetChild(0).gameObject);
        }
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print($"방에 들어왔어");
        exitButton.gameObject.SetActive(!PhotonNetwork.IsMasterClient);

        if (PhotonNetwork.IsMasterClient)
        {
            readyButtonText.text = "게임 시작";
            //todo :  이부분 OnJoinedRoom()으로 내리는게 맞을까?
            Instantiate(playerEntryPrefab, playerPoint[0], false);
        }
        else
        {
            readyButtonText.text = "준비";
            Instantiate(playerEntryPrefab, playerPoint[PhotonNetwork.CurrentRoom.PlayerCount - 1], false);
        }
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        print("방에 참여 실패");
    }

    void ReadyButtonClick()
    {
        if(!PhotonNetwork.IsMasterClient) 
        {
            //if(모두가 레디 상태일때)
            //PhotonNetwork.LoadLevel("Game");

            Player localPlayer = PhotonNetwork.LocalPlayer;
            PhotonHashtable customProps = localPlayer.CustomProperties;

            if (!playerReady)
                playerReady = true;
            else
                playerReady = false;

            customProps["Ready"] = playerReady;
            localPlayer.SetCustomProperties(customProps);
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
