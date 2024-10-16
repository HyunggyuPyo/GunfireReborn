using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    
    public int coin { get; set; }
    public int bonusCoin { get; set; }
    public int soulStone { get; set; }

    private void Awake()
    {
        Instance = this;
        coin = 0;
        soulStone = 0;
    }
    
    public void GetCoin(int count)
    {
        if(Random.Range(0, 101) <= bonusCoin)
        {
            count *= 2;
        }
        
        coin += count;
    }

    public void GetSoulStone(int count)
    {
        soulStone += count;
    }
}
