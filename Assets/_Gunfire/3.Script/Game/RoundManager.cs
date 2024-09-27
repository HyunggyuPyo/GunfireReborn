using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] monsters;

    private void Update()
    {
        
    }
}
/*
 몬스터를 다 잡았는지 검사 -> 다음 스테이지 몬스터 소환 -> 문 사라짐 -> 반복
 
 */