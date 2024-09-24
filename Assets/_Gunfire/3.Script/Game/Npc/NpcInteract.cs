using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcInteract : MonoBehaviour
{
    public GameObject uiPanel;
    LayerMask targetLayer;

    private void Awake()
    {
        targetLayer = (1 << LayerMask.NameToLayer("Player"));
    }
    private void OnTriggerEnter(Collider other)
    {
        if((targetLayer | (1<<other.gameObject.layer)) != targetLayer)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.F))
            uiPanel.SetActive(true);
        //todo 트리거에 들어가면 f누르면 강화탕 열린다고 표시하는 버튼 보여주고 누르면 ui띄우기 여유 있으 만들기
    }
}
