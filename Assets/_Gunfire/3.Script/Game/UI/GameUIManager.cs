using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager instance;

    public GameObject resultUI;
    public GameObject headShotUI;
    public GameObject gameoverPanel;
    public GameObject playerUI;
    public GameObject lodingUI;
    public GameObject pauseUI;
    public GameObject bossUI;

    public bool popUp { get; set; }
    Coroutine headShot = null;

    private void Awake()
    {
        instance = this;
        popUp = false;
    }

    private void Start()
    {
        lodingUI.SetActive(true);
    }

    private void Update()
    {
        if(!popUp && Input.GetKeyDown(KeyCode.Escape))
        {
            pauseUI.SetActive(true);
        }
    }

    public void SetResultUI()
    {
        gameoverPanel.SetActive(false);
        resultUI.SetActive(true);
    }

    public void HeadShotUI()
    {
        if(headShot == null)
        {
            headShot = StartCoroutine(HeadShot());
        }
        else
        {
            StopCoroutine(headShot);
            headShotUI.SetActive(false);
            headShot = null;
            headShot = StartCoroutine(HeadShot());
        }
        
    }
    public void GameOver()
    {
        playerUI.SetActive(false);
        gameoverPanel.SetActive(true);
    }

    IEnumerator HeadShot()
    {
        headShotUI.SetActive(true);
        yield return new WaitForSeconds(1f);
        headShotUI.SetActive(false);
        headShot = null;
    }
}
