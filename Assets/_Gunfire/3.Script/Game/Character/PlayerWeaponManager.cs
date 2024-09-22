using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    public static PlayerWeaponManager Instance;

    public Transform weapons;

    List<GameObject> guns = new List<GameObject>(3);
    List<int> bullet = new List<int>();
    int gunNum;
    int tempNum;
    Transform fieldDrop;
    public GameObject defaultGun; //todo �̰� ���⼭ ���ҽ� �̴�Ʈ �ϸ� �Ǵ°� �ƴѰ�?

    //GameObject hitGun;

    private void Awake()
    {
        Instance = this;
        gunNum = 3;
        fieldDrop =GameObject.Find("DropGunPrefab").transform;
        //hitGun = RayController.Instance.hit.collider.gameObject;
        guns.AddRange(new GameObject[] { null, null, defaultGun});

    }

    private void Update()
    {
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

        if(RayController.Instance.targetGun && Input.GetKeyDown(KeyCode.F))
        {
            PickUpGun(RayController.Instance.hit.collider.gameObject);
        }

    }

    void PickUpGun(GameObject hitGun)
    {
        if(guns[0] == null)
        {
            hitGun.transform.SetParent(weapons);
            guns[0] = hitGun;
            guns[gunNum - 1].SetActive(false);

            tempNum = gunNum;
            gunNum = 1;
            changeGun();
        }
        else if (guns[1] == null)
        {
            hitGun.transform.SetParent(weapons);
            guns[1] = hitGun;
            guns[gunNum - 1].SetActive(false);

            tempNum = gunNum;
            gunNum = 2;
            changeGun();
        }
        else if (gunNum == 3)
        {
            guns[0].transform.SetParent(fieldDrop);
            guns[0].SetActive(true);
            hitGun.transform.SetParent(weapons);
            guns[0] = hitGun;
            tempNum = gunNum;
            gunNum = 1;
            changeGun();
        }
        else
        {
            guns[gunNum - 1].transform.SetParent(fieldDrop);
            hitGun.transform.SetParent(weapons);
            guns[gunNum - 1] = hitGun;
            changeGun();
        }
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
    }

}
