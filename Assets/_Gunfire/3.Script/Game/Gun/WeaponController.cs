using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Gun myWeapon;
    Gun tempWeapon;
    public Transform startPosition;
    public GameObject particle;
    [HideInInspector]
    public bool wearing = false;

    private void Awake()
    {
        myWeapon.InitSetting();
    }

    private void Start()
    {
        tempWeapon = myWeapon;
        myWeapon.InitPool();
    }

    private void Update()
    {
        if(wearing)
        {
            if (tempWeapon != myWeapon)
            {
                tempWeapon = myWeapon;
                myWeapon.InitSetting();
            }

            myWeapon.Using(startPosition);
            myWeapon.ReLoad();
        }
    }

    public void SetParticle()
    {
        if(particle.activeSelf)
        {
            particle.SetActive(false);
        }
        else
        {
            particle.SetActive(true);
        }
    }
}
