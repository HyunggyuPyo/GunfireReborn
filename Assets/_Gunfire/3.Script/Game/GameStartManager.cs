using Photon.Pun;
using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartManager : MonoBehaviour
{
    public Transform startPosition;

    private void Start()
    {
        if(PhotonNetwork.InRoom)
        {
            StartCoroutine(NormalStart());
        }
        else
        {
            StartCoroutine(DebugStart());
        }
    }

    IEnumerator NormalStart()
    {
        yield return new WaitUntil(() => PhotonNetwork.LocalPlayer.GetPlayerNumber() != -1);

        int playerNumber = PhotonNetwork.LocalPlayer.GetPlayerNumber();
        Transform playerPos = startPosition.GetChild(playerNumber);
        GameObject playerObj = PhotonNetwork.Instantiate("LocalFox", playerPos.position, playerPos.rotation);
        //RemoteFox
        //playerObj.name = FirebaseManager.Instance.userData.userName;
    }

    public static bool debugReady = false;

    IEnumerator DebugStart()
    {
        gameObject.AddComponent<PhotonDebuger>();

        yield return new WaitUntil(() => debugReady);

        StartCoroutine(NormalStart());
    }

    
}
