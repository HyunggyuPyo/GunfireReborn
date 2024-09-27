using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleRat : Enemy
{
    bool canAtk = false;
    public GameObject attackParticle;

    private void OnEnable()
    {
        InitSetting();
        FindPlayer();
    }

    private void Update()
    {
        if (!isDead)
        {
            agent.SetDestination(target.position);

            if (agent.velocity.magnitude > 0)
            {
                animator.SetBool("Move", true);
                canAtk = true;
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

    public override IEnumerator Attack()
    {
        animator.SetTrigger("Atk");
        agent.speed = 0f;

        yield return new WaitForSeconds(3f); //2.5
        gameObject.SetActive(false);

    }

    public void OnParticle()
    {
        attackParticle.SetActive(true);
    }
}
