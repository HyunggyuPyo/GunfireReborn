using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    LayerMask targetLayer;
    int damage;

    private void Awake()
    {
        targetLayer = (1 << LayerMask.NameToLayer("Player"));
        damage = transform.root.gameObject.GetComponent<Enemy>().MonsterData.damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((targetLayer | (1 << other.gameObject.layer)) != targetLayer)
        {
            return;
        }

        if (other.TryGetComponent<IHitable>(out IHitable hitable))
        {
            hitable.Hit(damage);
            print($"bigmolerat -> {other.name} , damage : {damage}");
            //todo 데미지 텍스트 띄우고 
        }
    }
}
