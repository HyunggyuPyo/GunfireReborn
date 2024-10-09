using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager instance;

    public GameObject resultUI;
    public GameObject headShotAim;
    public GameObject shotAim;
    public GameObject gameoverPanel;
    public GameObject playerUI;
    public GameObject lodingUI;
    public GameObject pauseUI;
    public GameObject bossUI;

    public bool popUp { get; set; }
    Coroutine headShot, shot = null;

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
            headShot = StartCoroutine(HeadShot(headShotAim, headShot));
        }
        else
        {
            StopCoroutine(headShot);
            headShotAim.SetActive(false);
            headShot = null;
            headShot = StartCoroutine(HeadShot(headShotAim, headShot));
        }
    }

    public void ShotUI()
    {
        if (shot == null)
        {
            shot = StartCoroutine(HeadShot(shotAim, shot));
        }
        else
        {
            StopCoroutine(shot);
            shotAim.SetActive(false);
            shot = null;
            shot = StartCoroutine(HeadShot(shotAim, shot));
        }
    }

    public void GameOver()
    {
        playerUI.SetActive(false);
        gameoverPanel.SetActive(true);
    }

    IEnumerator HeadShot(GameObject aim, Coroutine coroutine)
    {
        aim.SetActive(true);
        yield return new WaitForSeconds(.5f);
        aim.SetActive(false);
        coroutine = null;
    }
}
