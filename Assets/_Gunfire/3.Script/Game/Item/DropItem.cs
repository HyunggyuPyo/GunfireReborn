using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public Item item;
    Rigidbody rigid;
    LayerMask targetLayer;
    bool pull = false;
    Transform targetTransform;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        targetLayer = (1 << LayerMask.NameToLayer("Player"));
    }

    private void OnEnable()
    {
        rigid.AddForce(new Vector3(Random.Range(-100f, 100f), 200, Random.Range(-100f, 100f)));
    }

    private void Update()
    {
        if(pull)
        {
            PullItem();
        }
    }

    public void SetItem(ItemSO _data, int _amount = 1)
    {
        item = new Item(_data, _amount);
    }

    private void OnTriggerEnter(Collider other)
    {
        if((targetLayer | (1 << other.gameObject.layer)) != targetLayer)
        { 
            return;
        }

        if(pull == false)
        {
            rigid.velocity = Vector3.zero;
            pull = true;
        }

        targetTransform = other.gameObject.transform;
    }

    void PullItem()
    {
        transform.LookAt(targetTransform);
        rigid.AddForce(transform.forward * 10f, ForceMode.VelocityChange);

        float distance = Vector3.Distance(transform.position, targetTransform.position);
        if(distance < 1.3f)
        {
            switch (item.data.id)
            {
                case 0:
                    targetTransform.gameObject.GetComponent<Inventory>().GetCoin(item.amount);
                    gameObject.SetActive(false);
                    break;
                case 1:
                    targetTransform.gameObject.GetComponent<Inventory>().GetSoulStone(item.amount);
                    gameObject.SetActive(false);
                    break;
                default:
                    break;
            }
        }
    }
}
