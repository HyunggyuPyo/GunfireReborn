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

    public Image dushCool;
    public TMP_Text count;

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


    public IEnumerator CoolTime()
    {
        dushCool.gameObject.SetActive(true);
        float cool = 2;
        dushCool.fillAmount = 1;

        while(cool >= 0)
        {
            cool -= Time.deltaTime;
            dushCool.fillAmount = cool / 2;
            count.text = ((int)cool + 1).ToString();
            yield return new WaitForFixedUpdate();
        }

        dushCool.gameObject.SetActive(false);
    }
}