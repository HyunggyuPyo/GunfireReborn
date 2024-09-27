using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMoleRat : Enemy
{
    bool canAtk = true;
    public GameObject attackParticle;

    private void OnEnable()
    {
        InitSetting();
        FindPlayer();
        print($"MoleRat Target => {target}");
    }

    private void Update()
    {
        
        if (!isDead)
        {
            if (IsReach() > MonsterData.detection)
            {
                if (Random.Range(0, 200) == 0)
                {
                    transform.Rotate(new Vector3(0, Random.Range(0, 361), 0));
                    if (move != null)
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
            else
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

        yield return new WaitForSeconds(2f); //1
        canAtk = true;
    }

    public void OnParticle()
    {
        attackParticle.SetActive(true);
    }

    public void OffParticle()
    {
        attackParticle.SetActive(false);
    }

}
