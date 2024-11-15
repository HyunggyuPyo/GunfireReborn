using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyUI : MonoBehaviour
{
    public TMP_Text enemyName;
    public Slider hp;

    public GameObject targetObj { get; set; }

    private void OnEnable()
    {
        if(targetObj != null)
        {
            enemyName.text = targetObj.GetComponent<Enemy>().name;
            hp.maxValue = targetObj.GetComponent<Enemy>().MonsterData.maxHealth;
        }
            
        StartCoroutine(OffObj());
    }

    private void OnDisable()
    {
        if(targetObj != null)
        {
            EnemyUIController.Instance.uiDic.Remove(targetObj);
        }
        
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
