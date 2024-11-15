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

    public GameObject Popup;
    public TMP_Text popupMessage;

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
        SoundManager.instance.ButtonSoundPlay();

        loginButton.interactable = false;

        FirebaseManager.Instance.Login(idInput.text, pwInput.text, (user) =>
        {
            loginButton.interactable = true;
            PhotonNetwork.ConnectUsingSettings();
        },
        (eMessage) =>
        {
            Popup.SetActive(true);
            popupMessage.text = "로그인 실패."; //eMessage.ToString()
            //todo 창 닫기 만들기 닉네임 중복 만들기 
            loginButton.interactable = true;
        });
    }


    public void SignUpButtinCilck()
    {
        SoundManager.instance.ButtonSoundPlay();

        signUpButton.interactable = false;

        FirebaseManager.Instance.SignUp(idInput.text, pwInput.text, (user) =>
        {
            loginPanel.SetActive(false);
            namePanel.SetActive(true);
            signUpButton.interactable = true;
        },
        (eMessage) =>
        {
            Popup.SetActive(true);
            popupMessage.text = "회원가입 실패."; //eMessage.ToString()
            signUpButton.interactable = true;
        });
    }

    public void SetNameButtonClick()
    {
        SoundManager.instance.ButtonSoundPlay();

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
