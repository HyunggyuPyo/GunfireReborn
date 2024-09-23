using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyUI : MonoBehaviour
{
    public TMP_Text enemyName;
    public Slider hp;

    [HideInInspector]
    public GameObject targetObj;

    private void OnEnable()
    {
        if(targetObj != null)
            enemyName.text = targetObj.GetComponent<Enemy>().name;
        StartCoroutine(OffObj());
        hp.maxValue = targetObj.GetComponent<Enemy>().MonsterData.maxHealth;
    }

    private void OnDisable()
    {
        EnemyUIController.Instance.uiDic.Remove(targetObj);
        StopCoroutine(OffObj());
    }

    private void Update()
    {
        hp.value = targetObj.GetComponent<Enemy>().hp;

    }

    IEnumerator OffObj()
    {
        yield return new WaitForSeconds(1.5f);

        gameObject.SetActive(false);
    }
}
