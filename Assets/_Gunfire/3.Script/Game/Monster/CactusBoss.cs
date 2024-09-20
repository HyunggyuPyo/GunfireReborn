using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusBoss : Enemy
{
    bool canAtk = true;
    public ObjectPool thornPool;
    public ObjectPool tornadoPool;

    private void OnEnable()
    {
        InitSetting();
        FindPlayer();

        StartCoroutine(Attack());
    }

    private void Update()
    {
        if(!isDead)
        {
            if(agent.enabled)
            {
                agent.SetDestination(target.position);
            }

            if (agent.velocity.magnitude > 0)
            {
                animator.SetBool("Move", true);
            }
            else
            {
                Quaternion lookTarget = Quaternion.LookRotation(target.position - transform.position);
                transform.rotation = Quaternion.Euler(0, lookTarget.eulerAngles.y, 0);
                animator.SetBool("Move", false);

                if (canAtk)
                {
                    canAtk = false;
                    animator.SetTrigger("Atk");
                }
            }
        }
    }

    void RandPattem()
    {
        float randomNum = Random.value;

        if (randomNum <= 0.3f)
        {
            StartCoroutine(Rush());
        }
        else if(randomNum <= 0.6f)
        {
            StartCoroutine(Tornado());
        }
        else
        {
            StartCoroutine(Shot());
        }
    }

    IEnumerator Rush()
    {
        animator.SetBool("Rush", true);
        Quaternion lookTarget = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Euler(0, lookTarget.eulerAngles.y, 0);

        float time = 0;

        while(time < .3f)
        {
            Vector3 targetPosition = transform.position + transform.forward * MonsterData.speed * 2f * Time.deltaTime;
            rigid.MovePosition(targetPosition);
            time += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(2f);
        rigid.velocity = Vector3.zero;
        animator.SetBool("Rush", false);

        canAtk = true;
        agent.enabled = true;
    }

    IEnumerator Tornado()
    {
        animator.SetTrigger("Spell");
        yield return new WaitForSeconds(.7f);
        tornadoPool.SpawnObj();

        yield return new WaitForSeconds(1.5f);
        canAtk = true;
        agent.enabled = true;
    }

    IEnumerator Shot()
    {
        animator.SetTrigger("Shot");
        yield return new WaitForSeconds(.5f);
        thornPool.SpawnObj();

        yield return new WaitForSeconds(1.5f);
        canAtk = true;
        agent.enabled = true;
    }

    public override IEnumerator Attack()
    {
        while(!isDead)
        {
            if (canAtk)
            {
                canAtk = false;
                agent.enabled = false;
                RandPattem();
            }
                
            yield return new WaitForSeconds(7f);
        }
    }

    public override void Move()
    {
        
    }
}
