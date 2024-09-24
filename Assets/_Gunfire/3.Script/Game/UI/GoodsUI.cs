using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GoodsUI : MonoBehaviour
{
    public TMP_Text coin;
    public TMP_Text soul;

    private void Update()
    {
        if(GameManager.Instance.isConnect)
        {
            coin.text = Inventory.Instance.coin.ToString();
            soul.text = Inventory.Instance.soulStone.ToString();
        }
    }
}
