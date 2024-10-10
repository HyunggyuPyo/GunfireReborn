using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class SkillIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image iconImage;
    public TMP_Text level;

    public SkillSO data;
    public GameObject skillPopup;

    Button buyButton;
    SkillPanel skillPanel;

    void Awake()
    {
        buyButton = GetComponent<Button>();
        buyButton.onClick.AddListener(BuyButtonClick);
        skillPanel = transform.GetComponentInParent<SkillPanel>();
    }

    void OnEnable()
    {
        iconImage.sprite = data.image;
        level.text = $"{FirebaseManager.Instance.skillData[data.type]}/{data.maxLevel}";
    }
    //todo awake때 멕스는 다 깔아두고 level만 onenable에서 매번 업데이트?

    public void OnPointerEnter(PointerEventData eventData)
    {
        skillPopup.SetActive(true);
        skillPopup.GetComponent<SkillPopup>().Setting(data);
        // todo 패널 위치 이동
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        skillPopup.SetActive(false);
    }

    public void BuyButtonClick()
    {
        SoundManager.instance.ButtonSoundPlay();

        if (data.price <= FirebaseManager.Instance.userData.soulPoint && data.maxLevel > FirebaseManager.Instance.skillData[data.type])
        {
            FirebaseManager.Instance.userData.soulPoint -= data.price;
            //todo 파이어베이스 데이터 값 바뀌면 반응 하는 액션 콜백 만들어거 데이터 바로 수정
            FirebaseManager.Instance.SetSkill(data.type, SetIcon);
        }
    }

    void SetIcon()
    {
        level.text = $"{FirebaseManager.Instance.skillData[data.type]}/{data.maxLevel}";
        skillPanel.soulCount.text = FirebaseManager.Instance.userData.soulPoint.ToString();
    }
}
