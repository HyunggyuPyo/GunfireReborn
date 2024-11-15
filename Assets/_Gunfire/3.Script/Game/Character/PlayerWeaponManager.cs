using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    public static PlayerWeaponManager Instance;

    public Transform weapons;

    List<GameObject> guns = new List<GameObject>(3);
    public List<GameObject> Guns { get { return guns; } set { guns = value; } }
    int gunNum;
    int tempNum;
    Transform fieldDrop;
    public GameObject defaultGun; //todo 이거 여기서 리소스 이니트 하면 되는거 아닌가?
    public int damage { get; set; }
    public int bonusDamage { get; set; }
    public float distance { get; private set; }

    private void Awake()
    {
        Instance = this;
        gunNum = 3;
        bonusDamage = 0;
        fieldDrop = GameObject.Find("DropGunPrefab").transform;
        guns.AddRange(new GameObject[] { null, null, defaultGun});
        damage = defaultGun.GetComponent<Gun>().data.damage;
        if(damage != 0)
        {
            damage *= 1 + (bonusDamage / damage);
        }        
    }

    private void Start()
    {
        guns[2].GetComponent<WeaponController>().wearing = true;
        
    }

    private void Update()
    {
        RayController.Instance.startPoint = guns[gunNum - 1].GetComponent<WeaponController>().startPosition.position;

        #region keydown Weapon Change
        if (Input.GetKeyDown(KeyCode.Alpha1) && guns[0] != null)
        {
            tempNum = gunNum;
            gunNum = 1;
            changeGun();
        }

        if(Input.GetKeyDown(KeyCode.Alpha2) && guns[1] != null)
        {
            tempNum = gunNum;
            gunNum = 2;
            changeGun();
        }

        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            tempNum = gunNum;
            gunNum = 3;
            changeGun();
        }
        #endregion

        if (RayController.Instance.targetGun)
        {
            distance = Vector3.Distance(transform.position, RayController.Instance.hit.collider.gameObject.transform.position);
            
            if (distance < 2f && Input.GetKeyDown(KeyCode.F))
            {
                PickUpGun(RayController.Instance.hit.collider.gameObject);
            }            
        }

    }

    void PickUpGun(GameObject hitGun)
    {
        if (guns[0] == null)
        {
            hitGun.transform.SetParent(weapons);
            guns[0] = hitGun;
            guns[0].GetComponent<WeaponController>().wearing = true;
            guns[0].GetComponent<WeaponController>().SetParticle();
            guns[gunNum - 1].SetActive(false);

            tempNum = gunNum;
            gunNum = 1;
            changeGun();
        }
        else if (guns[1] == null)
        {
            hitGun.transform.SetParent(weapons);
            guns[1] = hitGun;
            guns[1].GetComponent<WeaponController>().wearing = true;
            guns[1].GetComponent<WeaponController>().SetParticle();
            guns[gunNum - 1].SetActive(false);

            tempNum = gunNum;
            gunNum = 2;
            changeGun();
        }
        else if (gunNum == 3)
        {
            guns[0].GetComponent<WeaponController>().wearing = false;
            guns[0].GetComponent<WeaponController>().SetParticle();
            guns[0].transform.SetParent(fieldDrop);
            guns[0].SetActive(true);

            hitGun.transform.SetParent(weapons);
            guns[0] = hitGun;
            guns[0].GetComponent<WeaponController>().wearing = true;
            guns[0].GetComponent<WeaponController>().SetParticle();
            tempNum = gunNum;
            gunNum = 1;
            changeGun();
        }
        else
        {
            guns[gunNum - 1].transform.SetParent(fieldDrop);
            guns[gunNum - 1].GetComponent<WeaponController>().wearing = false;
            guns[gunNum - 1].GetComponent<WeaponController>().SetParticle();

            hitGun.transform.SetParent(weapons);
            guns[gunNum - 1] = hitGun;
            guns[gunNum - 1].GetComponent<WeaponController>().wearing = true;
            guns[gunNum - 1].GetComponent<WeaponController>().SetParticle();
            changeGun();
        }
        InitTransform(hitGun);
    }

    void changeGun()
    {
        if(tempNum != gunNum)
            guns[tempNum - 1].SetActive(false);

        switch (gunNum)
        {
            case 1:
                guns[0].SetActive(true);
                break;
            case 2:
                guns[1].SetActive(true);
                break;
            case 3:
                guns[2].SetActive(true);
                break;
        }

        PlayerWeaponUI.Instance.ImageChange(guns[gunNum - 1].GetComponent<Gun>().data.image);
        damage = guns[gunNum - 1].GetComponent<Gun>().data.damage + (guns[gunNum - 1].GetComponent<Gun>().data.level * 2);
        damage *= 1 + (bonusDamage / damage);
        print($"damage => {damage}");
        PlayerWeaponUI.Instance.SetBulletCount(guns[gunNum - 1].GetComponent<Gun>().bulletCount, guns[gunNum - 1].GetComponent<Gun>().data.maxBullet);
        //RayController.Instance.startPoint = guns[gunNum - 1].GetComponent<WeaponController>().startPosition.position;
    }

    public void InitSetting()
    {
        if(guns[gunNum - 1].GetComponent<Gun>().data.image)
        {
            print("이미지 확인");
        }
        PlayerWeaponUI.Instance.ImageChange(guns[gunNum - 1].GetComponent<Gun>().data.image);
        damage = guns[gunNum - 1].GetComponent<Gun>().data.damage + (guns[gunNum - 1].GetComponent<Gun>().data.level * 2);
        damage *= 1 + (bonusDamage / damage);
        print($"damage => {damage}");
    }

    void InitTransform(GameObject gun)
    {
        gun.transform.localPosition = Vector3.zero;
        gun.transform.localRotation = Quaternion.identity;
    }

    public Sprite GetGunImage()
    {
        Sprite gun = guns[gunNum -1].GetComponent<Gun>().data.image;
        return gun;
    }

    public void ResetWeapon()
    {
        foreach (Transform child in weapons)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
