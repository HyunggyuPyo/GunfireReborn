using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleRat : Enemy
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
        IsReach();

        Quaternion lookTarget = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Euler(0, lookTarget.eulerAngles.y, 0);

        if (distance >= 2.7f && canAtk)
        {
            animator.SetBool("Move", true);
            Vector3 targetPosition = transform.position + transform.forward * MonsterData.speed * Time.deltaTime;
            rigid.MovePosition(targetPosition);
        }
        else
        {
            animator.SetBool("Move", false);
            if(canAtk)
            {
                canAtk = false;
                StartCoroutine(Attack());
            }   
        }
    }

    public override IEnumerator Attack()
    {
        animator.SetTrigger("Atk");

        yield return new WaitForSeconds(2f); //1
        canAtk = true;
    }

    public override void Move()
    {
        
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
