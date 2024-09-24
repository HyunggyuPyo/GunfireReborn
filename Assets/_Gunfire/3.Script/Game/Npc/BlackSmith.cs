using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackSmith : MonoBehaviour
{
    public GameObject leftPreset;
    public GameObject rightPreset;

    // �� ���� ���� Ȯ�� �� Ȱ��ȭ

    // ���� �� �� ���� ������Ʈ�� ���� ������Ʈ ����� �ű⼭ ������ �������⤿

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
