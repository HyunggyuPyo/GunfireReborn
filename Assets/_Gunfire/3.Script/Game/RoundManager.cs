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
 몬스터를 다 잡았는지 검사 -> 다음 스테이지 몬스터 소환 -> 문 사라짐 -> 반복
  이 스크립트를 포탈에 달아서 검사 
 */