using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : MonoBehaviour
{
    public InputField idInput;
    public InputField pwInput;

    public Button loginButton;
    public Button signUpButton;

    public GameObject namePanel;

    private void Awake()
    {
        loginButton.onClick.AddListener(LoginButtonClick);
    }

    IEnumerator Start()
    {
        idInput.interactable = false;
        pwInput.interactable = false;
        loginButton.interactable = false;
        signUpButton.interactable = false;

        yield return new WaitUntil(() => FirebaseManager.Instance.IsInitialized);

        idInput.interactable = true;
        pwInput.interactable = true;
        loginButton.interactable = true;
        signUpButton.interactable = true;
    }

    public void LoginButtonClick()
    {
        loginButton.interactable = false;

        //FirebaseManager.Instance
    }

    public void SignUpButtinCilck()
    {
        signUpButton.interactable = false;

        FirebaseManager.Instance.SignUp(idInput.text, pwInput.text, (user) =>
        {

            signUpButton.interactable = true;
        });
    }
}
