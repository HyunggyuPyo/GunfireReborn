using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class ResultPanel : MonoBehaviour
{
    public TMP_Text title;
    public TMP_Text clearText;
    public TMP_Text maxDamage;
    public Image gun;
    public TMP_Text enemyCount;
    public TMP_Text coinCount;
    public TMP_Text soulPoint;
    public RawImage characterRender;
    public Button lobbyButton;

    private void Awake()
    {
        lobbyButton.onClick.AddListener(LobbyButtonClick);
    }

    private void OnEnable()
    {
        if(GameManager.Instance.clear)
        {
            clearText.text = "SUCCESS";
            title.color = new Color(255f / 255f, 145f / 255f, 47f / 255f);
            clearText.color = new Color(255f / 255f, 145f / 255f, 47f / 255f);
        }
        else
        {
            clearText.text = "FAILED";
            title.color = new Color(125f / 255f, 125f / 255f, 125f / 255f);
            clearText.color = new Color(125f / 255f, 125f / 255f, 125f / 255f);
        }

        maxDamage.text = GameManager.Instance.maxDamage.ToString();
        gun.sprite = PlayerWeaponManager.Instance.GetGunImage();
        enemyCount.text = GameManager.Instance.enemyCount.ToString();
        coinCount.text = Inventory.Instance.coin.ToString();
        soulPoint.text = $"{FirebaseManager.Instance.userData.soulPoint} +{Inventory.Instance.soulStone}";

        CharacterManager.instance.characterPrefab.SetActive(true);
        CharacterManager.instance.characterPrefab.GetComponent<FoxResultAnimator>().ChangeAnimation(GameManager.Instance.clear);
    }

    void LobbyButtonClick()
    {
        PhotonNetwork.LoadLevel("Robby");
    }
}
