using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxMainSkill : MonoBehaviour
{
    public GameObject orb;
    public GameObject chain;

    int damage;
    float speed = 6f;
    Rigidbody rigid;
    Collider sphere;
    Vector3 dir;
    LayerMask targetLayer;
    bool bondage = false; 
    Coroutine despawnCoroutine = null;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        sphere = GetComponent<SphereCollider>();
        targetLayer = (1 << LayerMask.NameToLayer("Enemy")) | (1 << LayerMask.NameToLayer("Boss"));
        damage = transform.GetComponentInParent<CharacterSkill>().data.mainSkillDamage;
    }

    private void OnEnable()
    {
        dir = RayController.Instance.hitObj.point;

        if (dir == Vector3.zero)
        {
            dir = RayController.Instance.centerPosition;
            transform.forward = dir;
        }
        else
        {
            transform.LookAt(dir);
        }

        var extinction = StartCoroutine(ExtinctionOrb());
        despawnCoroutine = StartCoroutine(DespawnSkill(extinction));
    }

    private void Update()
    {
        if(!bondage)
        {
            rigid.AddForce(transform.forward * speed, ForceMode.VelocityChange);
        }        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if ((targetLayer | (1 << other.gameObject.layer)) != targetLayer)
        {
            return;
        }

        if(despawnCoroutine != null)
        {
            StopCoroutine(despawnCoroutine);
        }
        
        bondage = true;
        orb.SetActive(false);
        chain.SetActive(true);
        sphere.enabled = false;

        if (other.TryGetComponent<IHitable>(out IHitable hitable))
        {
            hitable.Hit(damage);
        }

        rigid.velocity = Vector3.zero;

        transform.SetParent(other.transform);
        transform.localPosition = Vector3.zero;
        var enemyBondage = StartCoroutine(other.gameObject.GetComponent<Enemy>().Bondage());
        despawnCoroutine = StartCoroutine(DespawnSkill(enemyBondage));
    }
    IEnumerator DespawnSkill(Coroutine bondageCoroutine)
    {
        yield return bondageCoroutine; 

        Destroy(gameObject);
    }

    IEnumerator ExtinctionOrb()
    {
        yield return new WaitForSeconds(2f);
    }
}
