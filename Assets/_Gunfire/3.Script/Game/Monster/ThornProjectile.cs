using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornProjectile : MonoBehaviour
{
    float speed = 6f;
    Rigidbody rigid;
    LayerMask targetLayer;
    int damage;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        targetLayer = (1 << LayerMask.NameToLayer("Player"));
        damage = transform.root.gameObject.GetComponent<Enemy>().MonsterData.damage;
    }

    private void OnEnable()
    {
        StartCoroutine(DespawnThorn());
    }

    private void Update()
    {
        rigid.AddForce(transform.forward * speed, ForceMode.VelocityChange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if((targetLayer | (1 << other.gameObject.layer)) != targetLayer)
        {
            return;
        }

        if(other.TryGetComponent<IHitable>(out IHitable hitable))
        {
            hitable.Hit(damage);
            print($"가시가 플레이어에게 준 대미지 : {damage}");
        }

        gameObject.SetActive(false);
    }

    IEnumerator DespawnThorn()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
