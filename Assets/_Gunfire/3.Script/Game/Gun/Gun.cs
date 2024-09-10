using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GunData
{
    public float delay;
    public int maxBullet;
    public AudioSource soundEffect;
    public string info;
    public GameObject bullet;
}

public abstract class Gun : MonoBehaviour
{
    public GunData data;

    bool shootAble = true;
    float restTime = 0;

    int pointer;
    List<GameObject> pool = new List<GameObject>();

    public abstract void InitSetting();

    public virtual void InitPool()
    {
        for (int i = 0; i < data.maxBullet / 4; i++)
        {
            GameObject bulletObj = Instantiate(data.bullet, transform);
            bulletObj.SetActive(false);
            pool.Add(bulletObj);
        }

        pointer = 0;
    }

    public virtual void Using(Transform startPos)
    {
        if(Input.GetMouseButtonDown(0) && shootAble)
        {
            if (pointer != pool.Count)
            {
                pool[pointer].transform.position = startPos.position;
                pool[pointer].SetActive(true);
                pointer++;
            }
            else
            {
                pointer = 0;
                pool[pointer].transform.position = startPos.position;
                pool[pointer].SetActive(true);
                pointer++;
            }

            shootAble = false;

            data.maxBullet--;
        }

        if(shootAble == false)
        {
            restTime += Time.deltaTime;
            if(restTime >= data.delay)
            {
                shootAble = true;
                restTime = 0f;
            }
        }
    }
}
