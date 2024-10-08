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
    public Image mainSkillCool;
    public TMP_Text skillPoint;

    private void Awake()
    {
        Instance = this;
    }

    void HoldGunsMark()
    {
        numImage[0].gameObject.SetActive(true);
    }

    public void ImageChange(Sprite img) 
    {
        gunImage.sprite = img;
    }

    public void SetSkillPoint(int amount)
    {
        skillPoint.text = amount.ToString();
    }


    public IEnumerator DushCoolTime(float time)
    {
        dushCool.gameObject.SetActive(true);
        float cool = time;
        dushCool.fillAmount = 1;

        while(cool >= 0)
        {
            cool -= Time.deltaTime;
            dushCool.fillAmount = cool / time;
            count.text = ((int)cool + 1).ToString();
            yield return new WaitForFixedUpdate();
        }

        dushCool.gameObject.SetActive(false);
    }

    public IEnumerator MainSkillDelay(float time)
    {
        mainSkillCool.gameObject.SetActive(true);
        mainSkillCool.fillAmount = 1;
        float temp = time;

        while (temp >= 0)
        {
            temp -= Time.deltaTime;
            mainSkillCool.fillAmount = temp / time;
            yield return new WaitForFixedUpdate();
        }

        mainSkillCool.gameObject.SetActive(false);
    }
}