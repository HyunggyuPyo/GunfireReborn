using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    float speed = 8f;
    int damage; //todo �ѿ� ���̹����� �ְ� �װ� ���;� �� �׷��� ��ȭ�� ����� ����
    Rigidbody rigid;
    bool reach = false;
    LayerMask targetLayer;
    //Coroutine despawn;
    Vector3 dir;
    public GameObject particle;
    Transform startPosition;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        targetLayer = (1 << LayerMask.NameToLayer("Enemy"));
        startPosition = GetComponentInParent<WeaponController>().startPosition;
    }

    private void OnEnable()
    {
        
        dir = RayController.Instance.hitObj.point;
        
        if(dir == Vector3.zero)
        {
            dir = RayController.Instance.centerPosition;
            transform.forward = dir;
        }
        else
        {
            transform.LookAt(dir);
        }

        damage = PlayerWeaponManager.Instance.damage;

        StartCoroutine(DespawnBullet());
        reach = false;
        particle.SetActive(true);
    }

    private void OnDisable()
    {
        transform.position = startPosition.position;
        rigid.velocity = Vector3.zero;
        particle.SetActive(false);
        StopCoroutine(DespawnBullet());
    }

    private void Update()
    {
        if(!reach)
        {
            rigid.AddForce(transform.forward * speed, ForceMode.VelocityChange);
        } 
    }

    private void OnTriggerEnter(Collider other)
    {
        if((targetLayer | (1 << other.gameObject.layer)) != targetLayer)
        {
            return;
        }

        reach = true;

        if (other.TryGetComponent<IHitable>(out IHitable hitable))
        {
            hitable.Hit(damage);
            //todo ������ �ؽ�Ʈ ���� 
        }
        
        rigid.velocity = Vector3.zero;

        //despawn = null;
        gameObject.SetActive(false);
    }

    IEnumerator DespawnBullet()
    {
        yield return new WaitForSeconds(1.5f);
        //transform.position = startPosition.position;
        //despawn = null;
        gameObject.SetActive(false);
    }
}

/*
 * todo : 10%Ȯ�� ��Ű�� ���鋚 ����
 var randomNum = Random.value; => 0~1���� float�� ������ ���ڸ� �̾���
    if(randomNum < 0.1f) => �̰� 10%Ȯ���϶� 
 */
