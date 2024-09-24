using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReinforcementData : MonoBehaviour
{
    public Image gunImage;
    public TMP_Text gunName;
    public TMP_Text info;
    public TMP_Text price;
    public TMP_Text upgradeCount;
    public int num;

    public Button upgrade;

    private void Awake()
    {
        upgrade.onClick.AddListener(UpgradeButtonClick);
    }

    private void OnEnable()
    {
        //todo �̰� �� ���̳�?
        gunImage.sprite = PlayerWeaponManager.Instance.Guns[num].GetComponent<Gun>().data.image;
        gunName.text = $"{PlayerWeaponManager.Instance.Guns[num].GetComponent<Gun>().data.info} + {PlayerWeaponManager.Instance.Guns[num].GetComponent<Gun>().data.level}";
        info.text = 
            $"����� : {PlayerWeaponManager.Instance.Guns[num].GetComponent<Gun>().data.damage} \n" +
            $"������ : {PlayerWeaponManager.Instance.Guns[num].GetComponent<Gun>().data.delay} \n" +
            $"źâ �뷮 : {PlayerWeaponManager.Instance.Guns[num].GetComponent<Gun>().data.maxBullet}";

        price.text = (200 + (PlayerWeaponManager.Instance.Guns[num].GetComponent<Gun>().data.level * 100)).ToString();

    }
    
    void UpgradeButtonClick()
    {
        if(Inventory.Instance.coin >= (200 + (PlayerWeaponManager.Instance.Guns[num].GetComponent<Gun>().data.level * 100)))
        {
            Inventory.Instance.coin -= (200 + (PlayerWeaponManager.Instance.Guns[num].GetComponent<Gun>().data.level * 100));
            PlayerWeaponManager.Instance.Guns[num].GetComponent<Gun>().data.level += 1;
        }
    }
}
