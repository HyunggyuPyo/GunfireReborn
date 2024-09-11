using Photon.Pun;
using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartManager : MonoBehaviour
{
    public Transform startPosition;
    public Transform players;

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
        //playerObj.transform.SetParent(playerPos);
        playerObj.transform.SetParent(players);
        //playerObj.name = FirebaseManager.Instance.userData.userName;

        // todo : 로컬 아니면 만들어둘 ui프리팹(remote유저들 hp인터페이스)도 같이 소환 
    }

    public static bool debugReady = false;

    IEnumerator DebugStart()
    {
        gameObject.AddComponent<PhotonDebuger>();

        yield return new WaitUntil(() => debugReady);

        StartCoroutine(NormalStart());
    }

    
}
