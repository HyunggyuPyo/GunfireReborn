using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemSO data;
    public int amount;

    public Item(ItemSO _data, int _amount =1)
    {
        data = _data;
        amount = _amount;
    }
    
}
