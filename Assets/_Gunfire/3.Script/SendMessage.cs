using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SendMessage : MonoBehaviour
{
    public TMP_InputField nameINput;
    public Button sendButton;

    private void Awake()
    {
        sendButton.onClick.AddListener(SendButtonClick);
    }

    public void SendButtonClick()
    {
        Message msg = new Message()
        {
            sender = FirebaseManager.Instance.Auth.CurrentUser.UserId //todo : �̰� �г��� ���޷� �ٲ�� ��
        };
    }
}
