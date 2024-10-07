using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour, IHitable
{
    public static CharacterManager instance;
    public Character playerCharacter;
    CharacterData data; // shield는 뺴서 maxshield로 초기화 하는게 맞지 않나? 
    public CharacterData Data { get { return data; } set { data = value; } }

    Coroutine chargeShield;

    public GameObject characterPrefab;
    public GameObject localPrefab;
    public GameObject cameraPos;
    public GameObject cameraTarget;

    private void Awake()
    {
        instance = this;
        playerCharacter.InitSetting();
        data = playerCharacter.characterData;
        // todo : 나중에 재능으로 찍은 스텟 json으로 불러와서 data값에 더해주기
        PlayerInfoUI.Instance.InitUI(data.maxShield, data.maxHealth);
    }

    public void Hit(int damage)
    {
        print($"fox hit -> {damage}, hp : {data.hp}");
        if(chargeShield != null)
        {
            StopCoroutine(chargeShield);
        }        

        if(data.shield < damage)
        {
            if(data.shield != 0)
            {
                data.hp -= damage - data.shield;
                data.shield = 0;
            }
            else
            {
                data.hp -= damage;
            }
        }
        else
        {
            data.shield -= damage; 
        }

        PlayerInfoUI.Instance.SetUI(data.shield, data.hp);

        if(data.shield < data.maxShield)
        {
            if(chargeShield != null)
            {
                chargeShield = null;
            }
            chargeShield = StartCoroutine(ChargeShield());
        }        

        if(data.hp <= 0)
        {
            StopCoroutine(chargeShield);
            PlayerDead();
        }
    }

    public void PlayerDead()
    {
        gameObject.GetComponent<LocalPlayerMove>().isAlive = false;
        PlayerWeaponManager.Instance.weapons.gameObject.SetActive(false);
        localPrefab.SetActive(false);
        cameraPos.transform.localPosition = new Vector3(-0.3f, 2f, -1.6f);
        cameraTarget.transform.localPosition = new Vector3(-0.3f, 0.57f, 0.3f);


        characterPrefab.SetActive(true);
        characterPrefab.GetComponent<FoxResultAnimator>().ChangeAnimation(GameManager.Instance.clear);
        StartCoroutine(GameManager.Instance.GameOver());
    }

    public void Interaction()
    {
        if(gameObject.GetComponent<LocalPlayerMove>().interaction)
        {
            gameObject.GetComponent<LocalPlayerMove>().interaction = false;
        }
        else
        {
            gameObject.GetComponent<LocalPlayerMove>().interaction = true;
        }
    }

    IEnumerator ChargeShield()
    {
        yield return new WaitForSeconds(3f);

        while (data.shield <= data.maxShield)
        {
            data.shield += 5;
            PlayerInfoUI.Instance.SetUI(data.shield, data.hp);
            yield return new WaitForSeconds(.7f);
        }

        data.shield = data.maxShield;
        chargeShield = null;
    }
}
