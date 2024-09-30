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
            //print($"���ð� �÷��̾�� �� ����� : {damage}");
        }

        StopCoroutine(DespawnThorn());
        gameObject.SetActive(false);
    }

    IEnumerator DespawnThorn()
    {
        yield return new WaitForSeconds(despawnTime); 
        //todo �ʹ� ���� ����� ĳ�������� ������ ���� �ø��� pool ũ�� �� �ø��� 
        gameObject.SetActive(false);
    }
}
