using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    float speed = 5f;
    int damage = 10; //todo �ѿ� ���̹����� �ְ� �װ� ���;� �� �׷��� ��ȭ�� ����� ����
    Rigidbody rigid;
    bool reach = false;
    LayerMask targetLayer;
    //Coroutine despawn;
    Vector3 dir;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();   
    }

    //private void Start()
    //{
    //    Camera mainCamera = Camera.main;
    //    Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

    //    dir = ray.direction;

    //    transform.forward = dir;
    //}

    private void OnEnable()
    {
        print(PickupGun.Instance.hitEnemy.point);
        dir = PickupGun.Instance.hitEnemy.point;
        //transform.forward = dir;
        transform.LookAt(dir);

        StartCoroutine(DespawnBullet());
        reach = false;
    }

    private void OnDisable()
    {
        rigid.velocity = Vector3.zero;
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
        yield return new WaitForSeconds(1f);

        //despawn = null;
        gameObject.SetActive(false);
    }
}
