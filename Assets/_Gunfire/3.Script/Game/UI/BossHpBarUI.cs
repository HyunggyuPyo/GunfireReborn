using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossHpBarUI : MonoBehaviour
{
    public TMP_Text bossName;
    public TMP_Text hp;
    public Slider hpBar;
    public Enemy bossData;

    private void OnEnable()
    {
        bossName.text = bossData.MonsterData.enemyName;
        hp.text = $"{bossData.hp}/{bossData.MonsterData.maxHealth}";
        hpBar.value = 1f;
    }

    private void Update()
    {
        hp.text = $"{bossData.hp}/{bossData.MonsterData.maxHealth}";

        if(bossData.hp <= 0)
        {
            Invoke("DisableObj", 3f);
        }
    }

    void DisableObj()
    {
        gameObject.SetActive(false);
    }
}
