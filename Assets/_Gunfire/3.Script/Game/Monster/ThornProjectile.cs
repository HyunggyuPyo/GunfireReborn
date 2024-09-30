using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornProjectile : MonoBehaviour
{
    public float despawnTime;
    public float speed;
    
    Rigidbody rigid;
    LayerMask targetLayer;
    int damage;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        targetLayer = (1 << LayerMask.NameToLayer("Player"));
        damage = transform.GetComponentInParent<Enemy>().MonsterData.damage;
    }

    private void OnEnable()
    {
        StartCoroutine(DespawnThorn());
    }

    private void OnDisable()
    {
        rigid.velocity = Vector3.zero;
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
            //print($"가시가 플레이어에게 준 대미지 : {damage}");
        }

        StopCoroutine(DespawnThorn());
        gameObject.SetActive(false);
    }

    IEnumerator DespawnThorn()
    {
        yield return new WaitForSeconds(despawnTime); 
        //todo 너무 빨리 사라짐 캐릭터한테 오지도 못함 늘리고 pool 크기 좀 늘리기 
        gameObject.SetActive(false);
    }
}
