using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tornado : MonoBehaviour
{
    LayerMask targetLayer;
    int damage;
    NavMeshAgent agent;
    Transform target;

    private void Awake()
    {
        targetLayer = (1 << LayerMask.NameToLayer("Player"));
        damage = transform.GetComponentInParent<Enemy>().MonsterData.damage;
        agent = GetComponent<NavMeshAgent>();
        target = transform.GetComponentInParent<Enemy>().target;
    }

    private void OnEnable()
    {
        StartCoroutine(DespawnTornado());
    }

    private void Update()
    {
        agent.SetDestination(target.position);
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
            print($"토네이도가 플레이어에게 준 대미지 : {damage}");
        }

        StopCoroutine(DespawnTornado());
        gameObject.SetActive(false);
    }

    IEnumerator DespawnTornado()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }
}
