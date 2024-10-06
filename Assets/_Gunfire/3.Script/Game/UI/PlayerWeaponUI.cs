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

    void HoldGunsMark()
    {
        numImage[0].gameObject.SetActive(true);
    }

    public void ImageChange(Sprite img) 
    {
        print("�̰� �ȴٰ�333?");
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

/*
   ��ɽ�ų ������   -> 0
 ������ ���̶� ��ȥ�� ȹ�� ȭ�� (���� ����ȭ��)  -> 0
 ���� ���� ����    -> 0
 �÷��̾� �ǵ� ȸ���� ���� -> 0
 �س��� �ߺ� �˻�
 ���� ȭ������ �����ϰ� ĳ���� �����Ǹ� �ݹ����� ȭ�� ������(�ε�â ó�� ���̱�) -> 0
 ���� �̹��� �׶��̼� �־��ֱ� ����) -> 0
 ��� ���� �ø��� -> 0 
 ��弦 ȿ�� �߰�  -> 0
 pause ����� 
 ���� ü�¹�
 */