using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoginPanel : MonoBehaviour
{
    public TMP_InputField idInput;
    public TMP_InputField pwInput;
    public TMP_InputField nameInput;

    public Button loginButton;
    public Button signUpButton;
    public Button setNameButton;

    public GameObject loginPanel;
    public GameObject namePanel;

    private void Awake()
    {
        loginButton.onClick.AddListener(LoginButtonClick);
        signUpButton.onClick.AddListener(SignUpButtinCilck);
        setNameButton.onClick.AddListener(SetNameButtonClick);
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

        FirebaseManager.Instance.Login(idInput.text, pwInput.text, (user) =>
        {
            loginButton.interactable = true;
            PhotonNetwork.ConnectUsingSettings();
        });
    }

    public void SignUpButtinCilck()
    {
        signUpButton.interactable = false;

        FirebaseManager.Instance.SignUp(idInput.text, pwInput.text, (user) =>
        {
            loginPanel.SetActive(false);
            namePanel.SetActive(true);
            signUpButton.interactable = true;
        });
    }

    public void SetNameButtonClick()
    {
        setNameButton.interactable = false;

        FirebaseManager.Instance.SetName(nameInput.text, () =>
        {
            setNameButton.interactable = true;
            namePanel.SetActive(false);
            loginPanel.SetActive(true);
            
            PhotonNetwork.ConnectUsingSettings();
        });
    }
}
