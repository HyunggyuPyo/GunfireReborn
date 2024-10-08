using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : Enemy
{
    bool canAtk = true;
    ObjectPool pool;

    private void OnEnable()
    {
        InitSetting();
        FindPlayer();
        print($"cactus Target => {target}");
        pool = GetComponent<ObjectPool>();
    }

    private void Update()
    {
        //IsReach();

        //Quaternion lookTarget = Quaternion.LookRotation(target.position - transform.position);
        //transform.rotation = Quaternion.Euler(0, lookTarget.eulerAngles.y, 0);

        //if(distance >= 8f)
        //{
        //    animator.SetBool("Move", true);
        //    Vector3 targetPosition = transform.position + transform.forward * MonsterData.speed * Time.deltaTime;
        //    rigid.MovePosition(targetPosition);
        //}
        //else
        //{
        //    animator.SetBool("Move", false);
        //    if(canAtk)
        //    {
        //        canAtk = false;
        //        StartCoroutine(Attack());
        //    }
        //}

        if(!isDead)
        {
            if (IsReach() > MonsterData.detection)
            {
                if (Random.Range(0, 220) == 0)
                {
                    transform.Rotate(new Vector3(0, Random.Range(0, 361), 0));
                    if(move != null)
                    {
                        StopCoroutine(move);
                        move = StartCoroutine(Move());
                    }
                    else
                    {
                        move = StartCoroutine(Move());
                    }                    
                }
            }
            else if(agent.enabled)
            {
                agent.SetDestination(target.position);

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
                        StartCoroutine(Attack());
                    }

                }
            }            
        }
    }

    public override IEnumerator Attack()
    {
        animator.SetTrigger("Atk");
        yield return new WaitForSeconds(.5f);
        pool.SpawnObj();
         
        yield return new WaitForSeconds(1.5f); 
        canAtk = true;
    }
}
