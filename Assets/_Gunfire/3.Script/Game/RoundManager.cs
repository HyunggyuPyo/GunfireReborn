using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public static RoundManager instace;

    [SerializeField]
    GameObject[] rounds;
    int pointer;

    private void Awake()
    {
        instace = this;
        pointer = 1;
    }

    public void ClearRound()
    {
        if(pointer != rounds.Length)
        {
            rounds[pointer].SetActive(true);
            pointer++;
        }  
        else
        {
            if(PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel("Boss");
            }
        }
    }
}
/*
 ���͸� �� ��Ҵ��� �˻� -> ���� �������� ���� ��ȯ -> �� ����� -> �ݺ�
  �� ��ũ��Ʈ�� ��Ż�� �޾Ƽ� �˻� 
 */

