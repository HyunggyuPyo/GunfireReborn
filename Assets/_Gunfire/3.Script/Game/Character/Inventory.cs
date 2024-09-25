using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    
    public int coin { get; set; }
    public int soulStone { get; set; }

    private void Awake()
    {
        Instance = this;
        coin = 2000; //todo 0으러 변경
        soulStone = 0;
    }
    
    public void GetCoin(int count)
    {
        coin += count;
    }

    public void GetSoulStone(int count)
    {
        soulStone += count;
    }
}
