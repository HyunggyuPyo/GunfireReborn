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
        pointer = 0;
    }

    public void ClearRound()
    {
        if(pointer != rounds.Length)
        {
            rounds[pointer + 1].SetActive(true);
            pointer++;
        }        
    }
}
/*
 ���͸� �� ��Ҵ��� �˻� -> ���� �������� ���� ��ȯ -> �� ����� -> �ݺ�
  �� ��ũ��Ʈ�� ��Ż�� �޾Ƽ� �˻� 
 */