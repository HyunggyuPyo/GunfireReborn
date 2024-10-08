using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxSubSkill : MonoBehaviour
{
    public GameObject orb;
    public GameObject skill;
    LayerMask targetLayer;
    Rigidbody rigid;

    float angle = 15f;
    float distance = 8f;


    public int damage { get; set; }

    private void Awake()
    {
        targetLayer = (1 << LayerMask.NameToLayer("Default")) | (1 << LayerMask.NameToLayer("Enemy")) | (1 << LayerMask.NameToLayer("Boss"));
        damage = transform.GetComponentInParent<CharacterSkill>().data.subSkillDamage;
        rigid = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Move();
    }

    private void Move()
    {
        Vector3 direction = transform.forward.normalized;

        float gravity = Physics.gravity.magnitude;
        float throwAngle = angle * Mathf.Deg2Rad;
        float velocity = Mathf.Sqrt(distance * gravity / Mathf.Sin(2 * throwAngle));

        Vector3 velocityVector = new Vector3(0, velocity * Mathf.Sin(throwAngle), velocity * Mathf.Cos(throwAngle));
        velocityVector = Quaternion.LookRotation(direction) * velocityVector;

        rigid.velocity = velocityVector;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if ((targetLayer | (1 << collision.gameObject.layer)) != targetLayer)
        {
            return;
        }
        rigid.velocity = Vector3.zero;
        transform.SetParent(null);
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                
        orb.gameObject.SetActive(false);
        skill.gameObject.SetActive(true);
        StartCoroutine(DespawnOrb());
    }

    IEnumerator DespawnOrb()
    {
        yield return new WaitForSeconds(3f);

        Destroy(gameObject);
    }
}
