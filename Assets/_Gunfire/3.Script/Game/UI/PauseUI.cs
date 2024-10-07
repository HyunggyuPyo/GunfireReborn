using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    public Button continueButton;
    public Button exitButton;

    private void Awake()
    {
        continueButton.onClick.AddListener(ContinueButinClick);
        exitButton.onClick.AddListener(ExitButtonClick);
    }

    private void OnEnable()
    {
        CharacterManager.instance.Interaction();
        GameUIManager.instance.playerUI.SetActive(false);
        Time.timeScale = 0;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ContinueButinClick();
        }
    }

    void ContinueButinClick()
    {
        gameObject.SetActive(false);
        CharacterManager.instance.Interaction();
        GameUIManager.instance.playerUI.SetActive(true);
        Time.timeScale = 1;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void ExitButtonClick()
    {
        ContinueButinClick();
        CharacterManager.instance.PlayerDead();
    }
}
