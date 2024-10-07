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
    // todo => EnemyUIController.Instance.inConnect 이거 따로 게임 플레이 매니저 만들어서 
    // 캐릭터가 매니저에 각각 스크립트에 연결됐음을 알리는 함수를 실행 시키게 바꾸면 안되나?
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
        info.text = $"대미지 : {target.GetComponent<Gun>().data.damage} \n" +
            $"딜레이 : {target.GetComponent<Gun>().data.delay} \n" +
            $"탄창 용량 : {target.GetComponent<Gun>().data.maxBullet}";

        panel.transform.position = RayController.Instance.mainCamera.WorldToScreenPoint(target.transform.position + new Vector3(0, 0.6f, 0));
    }
}
