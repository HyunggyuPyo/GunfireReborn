using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornProjectilePool : MonoBehaviour
{
    // todo �̸� make pool �����ɷ� �ٲٰ� ������ �� �޾Ƽ� �׳� pool ���� ���� ��ũ��Ʈ�� �ٲٱ� 
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
