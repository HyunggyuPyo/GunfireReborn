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
    //https://portable-paper.tistory.com/entry/3D-RPG-%EB%A7%8C%EB%93%A4%EA%B8%B0-6-%EB%AA%AC%EC%8A%A4%ED%84%B0%EC%95%84%EC%9D%B4%ED%85%9C-%EB%A7%8C%EB%93%A4%EA%B8%B0
}
