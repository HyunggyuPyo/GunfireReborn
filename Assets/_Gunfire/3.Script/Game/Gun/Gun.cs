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

    public abstract void InitSetting();

    public virtual void Using(Transform startPos)
    {
        if(Input.GetMouseButtonDown(0) && shootAble)
        {
            var bull = Instantiate(data.bullet);
            bull.transform.position = startPos.position;

            // 파티클도 동일한 방법으로 생성

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
