using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SendMessage : MonoBehaviour
{
    public TMP_InputField nameInput;
    public Button sendButton;
    public Button exitButton;

    private void Awake()
    {
        sendButton.onClick.AddListener(SendButtonClick);
        exitButton.onClick.AddListener(ExitButtonClick);
    }

    private void OnEnable()
    {
        if (!string.IsNullOrEmpty(nameInput.text))
        {
            nameInput.text = string.Empty;
        }
    }

    public void SendButtonClick()
    {
        //Message msg = new Message()
        //{
        //    sender = FirebaseManager.Instance.Auth.CurrentUser.UserId,
        //    nickname = FirebaseManager.Instance.userData.userName
        //};

        FirebaseManager.Instance.SendInvitation(nameInput.text, PhotonNetwork.CurrentRoom.Name);

        gameObject.SetActive(false);
    }

    public void ExitButtonClick()
    {
        gameObject.SetActive(false);
    }
}


