using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusBoss : Enemy
{
    bool canAtk = false;
    bool encounter = false;
    bool rush = false;
    public ObjectPool thornPool;
    public ObjectPool tornadoPool;
    public GameObject rushCollider;

    private void OnEnable()
    {
        InitSetting();
        FindPlayer();  
    }

    private void Update()
    {
        if(!isDead)
        {
            if (IsReach() <= MonsterData.detection && !encounter)
            {
                encounter = true;
                StartCoroutine(Attack());
                GameUIManager.instance.bossUI.GetComponent<BossHpBarUI>().bossData = GetComponent<Enemy>();
                GameUIManager.instance.bossUI.SetActive(true);
            }            

            if(encounter)
            {
                if (agent.enabled) //agent 안 끄는데 이거 왜 있는 조건문이지?
                {
                    agent.SetDestination(target.position);
                }

                if (agent.velocity.magnitude > 0) // || 로 돌진시 방향전환 안되게 변수 추가
                {
                    animator.SetBool("Move", true);
                }
                else
                {
                    animator.SetBool("Move", false);

                    if (canAtk)
                    {
                        return;
                    }

                    Quaternion lookTarget = Quaternion.LookRotation(target.position - transform.position);
                    transform.rotation = Quaternion.Euler(0, lookTarget.eulerAngles.y, 0);                    
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((targetLayer | (1 << collision.gameObject.layer)) != targetLayer)
        {
            return;
        }

        print("보스랑 충돌");
        if(rush)
        {
            if(collision.gameObject.TryGetComponent<IHitable>(out IHitable hitable))
            {
                hitable.Hit(MonsterData.damage);
            }
        }
    }

    void RandPattem() //todo 잡몹 소환 추가?
    {
        print("패턴 실행");
        float randomNum = Random.value;

        if (randomNum <= 0.3f)
        {
            StartCoroutine(Rush());
            print("rush 실행");
        }
        else if(randomNum <= 0.6f)
        {
            StartCoroutine(Tornado());
            print("tornado 실행");
        }
        else
        {
            StartCoroutine(Shot());
            print("shot 실행");
        }
    }

    public override IEnumerator Attack()
    {
        print("attack 실행");
        while (!isDead)
        {
            agent.speed = 0f;
            FindPlayer();
            RandPattem();

            yield return new WaitForSeconds(5f);
        }
    }

    IEnumerator Rush()
    {
        canAtk = true;
        animator.SetBool("Rush", true);
        Quaternion lookTarget = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Euler(0, lookTarget.eulerAngles.y, 0);
        rushCollider.SetActive(true);
        rush = true;
        float time = 0;

        while(time < .3f)
        {
            //Vector3 targetPosition = transform.position + transform.forward * 8f * Time.deltaTime;
            //rigid.MovePosition(targetPosition);
            rigid.AddForce(transform.forward * MonsterData.speed * 3f, ForceMode.VelocityChange);
            time += Time.deltaTime;
            yield return null;
        }

        rushCollider.SetActive(false);
        rush = false;
        animator.SetBool("Rush", false);
        yield return new WaitForSeconds(2f);
        //rigid.velocity = Vector3.zero;
        canAtk = false;
        agent.speed = 2f;
    }

    IEnumerator Tornado()
    {
        animator.SetTrigger("Spell");
        yield return new WaitForSeconds(.7f);
        tornadoPool.SpawnObj();

        yield return new WaitForSeconds(1.5f);
        agent.speed = 2f;
    }

    IEnumerator Shot()
    {
        animator.SetTrigger("Shot");
        yield return new WaitForSeconds(.5f);
        thornPool.SpawnObj();

        yield return new WaitForSeconds(1.5f);
        agent.speed = 2f;
    }

    //IEnumerator Smash()
    //{
    //    canAtk = true;
    //    agent.enabled = true;
    //    agent.speed = 6f;
    //    agent.stoppingDistance = 5f;

    //    yield return new WaitForSeconds(2f);
    //    agent.speed = 2f;
    //    agent.stoppingDistance = 13f;
    //}

}
