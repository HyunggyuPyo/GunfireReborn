using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropManager : MonoBehaviour
{
    public static ItemDropManager Instance;

    [SerializeField] GameObject p_Item;
    [SerializeField] Transform droppableItems;

    private void Awake()
    {
        Instance = this;
    }

    public void DropItemOnMonster(List<EnemyDropItem>_edi, Transform _transform)
    {
        for (int i = 0; i < _edi.Count; i++)
        {
            EnemyDropItem droppableItem = _edi[i];
            for (int j = 0; j < droppableItem.maxDropCount; j++)
            {
                if(Random.Range(0, 100) < droppableItem.percentage)
                {
                    DropItem(_transform, droppableItem.itemData, Random.Range(5, 11));
                    // pool 활성화
                }
            }
        }
    }

    public void DropItem(Transform _transform, ItemSO _itemData, int _amount = 1)
    {
        //todo pool 만들기
        GameObject cloneItem = Instantiate(_itemData.itemPrefab, _transform.position, _transform.rotation);
        cloneItem.transform.SetParent(droppableItems);
        //cloneItem.GetComponent<DropItem>().SetItem(_itemData, _amount);
        DropItem dropItem = cloneItem.GetComponent<DropItem>();
        if(dropItem != null)
        {
            dropItem.SetItem(_itemData, _amount);
        }
    }
}
