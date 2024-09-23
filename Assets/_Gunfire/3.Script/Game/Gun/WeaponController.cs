using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Gun myWeapon;
    Gun tempWeapon;
    public Transform startPosition;
    [HideInInspector]
    public bool wearing = false;

    private void Start()
    {
        tempWeapon = myWeapon;
        myWeapon.InitSetting();
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
}
