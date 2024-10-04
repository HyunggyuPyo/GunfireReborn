using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillPopup : MonoBehaviour
{
    public TMP_Text skillName;
    public Image icon;
    public TMP_Text level;
    public TMP_Text price;
    public TMP_Text tooltip;
    public TMP_Text[] levelTooltip;

    RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        for (int i = 0; i < levelTooltip.Length; i++)
        {
            levelTooltip[i].text = "";
        }
    }

    public void Setting(SkillSO data)
    {
        skillName.text = data.skillName;
        //icon.sprite = data.image;
        level.text = $"{FirebaseManager.Instance.skillData[data.type]}/{data.maxLevel}";
        price.text = data.price.ToString();
        tooltip.text = data.tooltip;

        for (int i = 0; i < data.levelTooltip.Length; i++)
        {
            levelTooltip[i].text = data.levelTooltip[i];
        }

        if(data.skillClass == SkillClass.quest)
        {
            rectTransform.anchoredPosition = new Vector2(-67, 0);
        }
        else if(data.skillClass == SkillClass.live)
        {
            rectTransform.anchoredPosition = new Vector2(-50, 0);
        }
        else
        {
            rectTransform.anchoredPosition = new Vector2(-346, 0);
        }
    }
}
