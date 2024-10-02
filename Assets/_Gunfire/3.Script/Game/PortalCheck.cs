using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCheck : MonoBehaviour
{
    [SerializeField]
    GameObject[] monsters;

    LayerMask targetLayer;
    bool inCollider = false;
    bool clear = false;
    public bool isBossRound = false;

    private void Awake()
    {
        targetLayer = (1 << LayerMask.NameToLayer("Player"));    
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && inCollider)
        {
            CheckAnyEnemyLive();

            if (clear)
            {
                //todo 여기서 파티워 레디 받아서 이동
                if(isBossRound == false)
                {
                    RoundManager.instace.ClearRound();
                }
                else
                {
                    GameManager.Instance.ReturnLobby();
                }
                
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if((targetLayer | (1 << other.gameObject.layer)) != targetLayer)
        {
            return;
        }

        inCollider = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if ((targetLayer | (1 << other.gameObject.layer)) != targetLayer)
        {
            return;
        }

        inCollider = false;
    }

    void CheckAnyEnemyLive()
    {
        foreach (GameObject monster in monsters)
        {
            if (monster.activeSelf)
            {
                break;
            }

            clear = true;
        }

        if(monsters.Length == 0)
        {
            clear = true;
        }
    }
}
/*
 몬스터를 다 잡았는지 검사 -> 다음 스테이지 몬스터 소환 -> 문 사라짐 -> 반복
  이 스크립트를 포탈에 달아서 검사 
 */