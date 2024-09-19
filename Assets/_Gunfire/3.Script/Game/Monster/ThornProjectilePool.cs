using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornProjectilePool : MonoBehaviour
{
    // todo 이름 make pool 같은걸로 바꾸고 변수로 다 받아서 그냥 pool 생성 공용 스크립트로 바꾸기 
    public GameObject prefab;
    public Transform parentObj;
    public Transform startPosition;

    int pointer;
    List<GameObject> pool = new List<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject obj = Instantiate(prefab, parentObj);
            obj.SetActive(false);
            pool.Add(obj);
        }

        pointer = 0;
    }

    public void ShotThorn()
    {
        if(pointer != pool.Count)
        {
            pool[pointer].transform.position = startPosition.position;
            pool[pointer].SetActive(true);
            pointer++;
        }
        else
        {
            pointer = 0;
            pool[pointer].transform.position = startPosition.position;
            pool[pointer].SetActive(true);
            pointer++;
        }
    }
}
