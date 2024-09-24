using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerWeaponUI : MonoBehaviour
{
    public static PlayerWeaponUI Instance;

    public Image[] numImage;
    public Image gunImage;
    public TMP_Text bulletCount;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {

    }

    void HoldGunsMark()
    {
        numImage[0].gameObject.SetActive(true);
    }

    public void ImageChange(Sprite img) 
    {
        gunImage.sprite = img;
    }

    
}
