using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcInteract : MonoBehaviour
{
    public GameObject uiPanel;
    LayerMask targetLayer;
    bool on = false;
    [HideInInspector]
    public int upgradeCount;
    public AudioClip npcClip;

    private void Awake()
    {
        targetLayer = (1 << LayerMask.NameToLayer("Player"));
    }

    private void Start()
    {
        //upgradeCount = 3;
        upgradeCount = PlayerDataManager.instance.upgradeCount;
    }

    private void Update()
    {
        if (on && uiPanel.activeSelf == false)
        {
            GameUIManager.instance.popUp = false;

            if (Input.GetKeyDown(KeyCode.F))
            {
                SoundManager.instance.SoundPlay("SmithSound", npcClip);
                uiPanel.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                CharacterManager.instance.Interaction();
                GameUIManager.instance.popUp = true;
            }
        }           

        if(uiPanel.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            uiPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            CharacterManager.instance.Interaction();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if((targetLayer | (1<<other.gameObject.layer)) != targetLayer)
        {
            return;
        }

        on = true;

        //todo 트리거에 들어가면 f누르면 강화탕 열린다고 표시하는 버튼 보여주고 누르면 ui띄우기 여유 있으 만들기
    }

    private void OnTriggerExit(Collider other)
    {
        if ((targetLayer | (1 << other.gameObject.layer)) != targetLayer)
        {
            return;
        }

        on = false;
    }
}
