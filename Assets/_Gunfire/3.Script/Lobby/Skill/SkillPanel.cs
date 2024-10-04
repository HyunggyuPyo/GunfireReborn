using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillPanel : MonoBehaviour
{
    public Button escButton;
    public Button okButton;

    public TMP_Text soulCount;

    private void Awake()
    {
        escButton.onClick.AddListener(EscButtonClick);
        okButton.onClick.AddListener(OkButtonClick);
    }

    private void OnEnable()
    {
        soulCount.text = FirebaseManager.Instance.userData.soulPoint.ToString();
    }

    void EscButtonClick()
    {
        gameObject.SetActive(false);
    }

    void OkButtonClick()
    {
        gameObject.SetActive(false);
    }
}
