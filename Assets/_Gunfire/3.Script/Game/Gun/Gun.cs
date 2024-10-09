using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct GunData
{
    public int level;
    public int damage;
    public float delay;
    public int maxBullet;
    
    public AudioSource soundEffect;
    public string info;
    public GameObject bullet;
    public Sprite image;
}

public abstract class Gun : MonoBehaviour
{
    public GunData data;
    public int bulletCount { get; set; }

    bool shootAble = true;
    bool reLoading = false;
    float restTime = 0;

    int pointer;
    List<GameObject> pool = new List<GameObject>();
    Animator parentAnimator;

    private void Awake()
    {
        parentAnimator = GetComponentInParent<Animator>();
    }

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
        if(Input.GetMouseButtonDown(0) && shootAble && !reLoading)
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

            bulletCount--;
            PlayerWeaponUI.Instance.SetBulletCount(bulletCount, data.maxBullet);
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

    public virtual void ReLoad()
    {
        if(parentAnimator == null)
        {
            parentAnimator = GetComponentInParent<Animator>();
        }
        // ∏≈∆ø∞— æ÷¥œ∏ﬁ¿Ãº¢
        if(Input.GetKeyDown(KeyCode.R) && data.maxBullet != bulletCount)
        {
            reLoading = true;
            parentAnimator.SetTrigger("ReLoad");
            bulletCount = data.maxBullet;
            StartCoroutine(Reloading());
        }
    }

    IEnumerator Reloading()
    {
        yield return new WaitForSeconds(1.7f);
        PlayerWeaponUI.Instance.SetBulletCount(bulletCount, data.maxBullet);
        reLoading = false;
    }
}
