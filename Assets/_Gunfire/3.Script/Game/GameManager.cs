using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instance;

    public bool isConnect { get; set; }

    private void Awake()
    {
        if(Instance == null)
            Instance = this;

        DontDestroyOnLoad(this);

        isConnect = false;
    }

    public void ReturnLobby()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Robby");
        }        
    }
}
