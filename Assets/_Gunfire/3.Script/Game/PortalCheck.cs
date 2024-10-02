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
                //todo ���⼭ ��Ƽ�� ���� �޾Ƽ� �̵�
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
 ���͸� �� ��Ҵ��� �˻� -> ���� �������� ���� ��ȯ -> �� ����� -> �ݺ�
  �� ��ũ��Ʈ�� ��Ż�� �޾Ƽ� �˻� 
 */