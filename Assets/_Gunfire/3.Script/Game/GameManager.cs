using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instance;

    public bool isConnect { get; set; }
    public bool clear { get; set; }

    public int enemyCount { get; set; }
    public int maxDamage { get; set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }        

        isConnect = false;
        clear = false;
        enemyCount = 0;
        maxDamage = 0;
    }

    public void ReturnLobby()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            GameUIManager.instance.SetResultUI();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }        
    }

    public IEnumerator GameOver()
    {
        clear = false;
        yield return new WaitForSeconds(1.5f);

        GameUIManager.instance.GameOver();
        yield return new WaitForSeconds(2f);

        ReturnLobby();
        Time.timeScale = 0;
    }
}
