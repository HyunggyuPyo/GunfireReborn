using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GunUI : MonoBehaviour
{
    public TMP_Text info;
    public Image gunImage;
    public GameObject panel;
    public TMP_Text gunName;

    GameObject temp;
    // todo => EnemyUIController.Instance.inConnect �̰� ���� ���� �÷��� �Ŵ��� ���� 
    // ĳ���Ͱ� �Ŵ����� ���� ��ũ��Ʈ�� ��������� �˸��� �Լ��� ���� ��Ű�� �ٲٸ� �ȵǳ�?
    private void Update()
    {
        if(GameManager.Instance.isConnect && RayController.Instance.targetGun)
        {
            if(PlayerWeaponManager.Instance.distance < 2f && RayController.Instance.hit.collider.gameObject != temp)
            {
                DataSet();
                panel.SetActive(true);
            }
        }
        else
        {
            panel.SetActive(false);
            temp = null;
        }
    }

    void DataSet()
    {
        GameObject target = RayController.Instance.hit.collider.gameObject;
        temp = target;
        gunName.text = $"{target.GetComponent<Gun>().data.info} + {target.GetComponent<Gun>().data.level}";
        gunImage.sprite = target.GetComponent<Gun>().data.image;
        info.text = $"����� : {target.GetComponent<Gun>().data.damage} \n" +
            $"������ : {target.GetComponent<Gun>().data.delay} \n" +
            $"źâ �뷮 : {target.GetComponent<Gun>().data.maxBullet}";

        panel.transform.position = RayController.Instance.mainCamera.WorldToScreenPoint(target.transform.position + new Vector3(0, 0.6f, 0));
    }
}
