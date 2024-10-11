using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpOff : MonoBehaviour
{
    public Button offButton;

    private void Awake()
    {
        offButton.onClick.AddListener(PopUpExit);
    }

    public void PopUpExit()
    {
        gameObject.SetActive(false);
    }
}
