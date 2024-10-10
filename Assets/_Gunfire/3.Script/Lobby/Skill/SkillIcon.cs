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
    //todo awake�� �߽��� �� ��Ƶΰ� level�� onenable���� �Ź� ������Ʈ?

    public void OnPointerEnter(PointerEventData eventData)
    {
        skillPopup.SetActive(true);
        skillPopup.GetComponent<SkillPopup>().Setting(data);
        // todo �г� ��ġ �̵�
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
            //todo ���̾�̽� ������ �� �ٲ�� ���� �ϴ� �׼� �ݹ� ������ ������ �ٷ� ����
            FirebaseManager.Instance.SetSkill(data.type, SetIcon);
        }
    }

    void SetIcon()
    {
        level.text = $"{FirebaseManager.Instance.skillData[data.type]}/{data.maxLevel}";
        skillPanel.soulCount.text = FirebaseManager.Instance.userData.soulPoint.ToString();
    }
}
