using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackSmith : MonoBehaviour
{
    public GameObject leftPreset;
    public GameObject rightPreset;

    // 총 존재 여부 확인 후 활성화

    // 이후 또 각 게임 오브젝트에 넣을 컴포넌트 만들고 거기서 데이터 가져오기ㅏ

    private void OnEnable()
    {
        if (PlayerWeaponManager.Instance.Guns[0] != null)
        {
            leftPreset.SetActive(true);
        }

        if (PlayerWeaponManager.Instance.Guns[1] != null)
        {
            rightPreset.SetActive(true);
        }
    }

    private void OnDisable()
    {
        leftPreset.SetActive(false);
        rightPreset.SetActive(false);
    }
}
