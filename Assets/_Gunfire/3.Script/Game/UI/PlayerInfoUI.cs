using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInfoUI : MonoBehaviour
{
    public static PlayerInfoUI Instance;

    public Slider shieldSlider;
    public Slider hpSlider;

    public TMP_Text shieldText;
    public TMP_Text hpText;

    private void Awake()
    {
        Instance = this;
    }

    public void InitUI(int sield, int hp)
    {
        shieldSlider.maxValue = sield;
        shieldSlider.value = sield;
        shieldText.text = $"{shieldSlider.value}  /  {shieldSlider.maxValue}";

        hpSlider.maxValue = hp;
        hpSlider.value = hp;
        hpText.text = $"{hpSlider.value}  /  {hpSlider.maxValue}";
    }

    public void SetUI(int sield, int hp)
    {
        shieldSlider.value = sield;
        shieldText.text = $"{shieldSlider.value}  /  {shieldSlider.maxValue}";

        hpSlider.value = hp;
        hpText.text = $"{hpSlider.value}  /  {hpSlider.maxValue}";
    }
}
