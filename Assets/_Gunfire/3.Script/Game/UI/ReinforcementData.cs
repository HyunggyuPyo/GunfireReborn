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
    NpcInteract blackSmith;
    public AudioClip buttonClip;

    private void Awake()
    {
        upgrade.onClick.AddListener(UpgradeButtonClick);
        blackSmith= gameObject.GetComponentInParent<NpcInteract>();
    }

    private void OnEnable()
    {
        GetInfo();
    }

    void GetInfo()
    {
        if (PlayerWeaponManager.Instance.Guns[num] != null)
        {
            GunData data = PlayerWeaponManager.Instance.Guns[num].GetComponent<Gun>().data;

            gunImage.sprite = data.image;
            gunName.text = $"{data.info} + {data.level}";
            info.text =
                $"´ë¹ÌÁö : {data.damage} + {data.level * 2} \n" +
                $"µô·¹ÀÌ : {data.delay} \n" +
                $"ÅºÃ¢ ¿ë·® : {data.maxBullet}";

            price.text = (200 + (data.level * 100)).ToString();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    
    void UpgradeButtonClick()
    {
        if(blackSmith.upgradeCount > 0 && Inventory.Instance.coin >= (200 + (PlayerWeaponManager.Instance.Guns[num].GetComponent<Gun>().data.level * 100)))
        {
            SoundManager.instance.SoundPlay("UpgradeClip", buttonClip);
            Inventory.Instance.coin -= (200 + (PlayerWeaponManager.Instance.Guns[num].GetComponent<Gun>().data.level * 100));
            PlayerWeaponManager.Instance.Guns[num].GetComponent<Gun>().data.level += 1;

            GetInfo();
            blackSmith.upgradeCount--;
            upgradeCount.text = $"ÀÜ¿© °­È­ È½¼ö : {blackSmith.upgradeCount}";
        }        
    }
}
