using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReceiveMessage : MonoBehaviour
{
    public TMP_Text msgText;
    public Button refusalButton; // ����
    public Button acceptanceButton; //����

    private string roomName;

    private void Awake()
    {
        refusalButton.onClick.AddListener(RefusalButtonClick);
        acceptanceButton.onClick.AddListener(AcceptanceButtonClick);
    }

    public void OnReceiveMessage(string inviter)
    {
        msgText.text = $"{inviter}�����κ��� �ʴ�����̽��ϴ�. \n ��Ƽ�� �����Ͻðڽ��ϱ�?";
        roomName = inviter;
        FirebaseManager.Instance.SendInvitation(FirebaseManager.Instance.userData.userId, " ");
        gameObject.SetActive(true);
    }

    public void RefusalButtonClick()
    {
        gameObject.SetActive(false);
    }

    public void AcceptanceButtonClick()
    {
        PhotonNetwork.JoinRoom(roomName);
        gameObject.SetActive(false);
    }
}
