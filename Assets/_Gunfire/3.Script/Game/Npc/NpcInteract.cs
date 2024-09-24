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
    }
}
