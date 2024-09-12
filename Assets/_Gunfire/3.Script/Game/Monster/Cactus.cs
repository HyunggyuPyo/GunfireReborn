using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : Monster
{
    bool canAtk = true;

    private void OnEnable()
    {
        InitSetting();
        FindPlayer();
        print($"cactus Target => {target}");
    }

    private void Update()
    {
        IsReach();

        Quaternion lookTarget = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Euler(0, lookTarget.eulerAngles.y, 0);

        if(distance >= 8f)
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
        // todo 가시 프리팹 발사 
        yield return new WaitForSeconds(1f);
        canAtk = true;
    }

    public override void Move()
    {
        
    }
}
