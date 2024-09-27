using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cacti : Enemy
{
    bool canAtk = true;
    ObjectPool pool;

    private void OnEnable()
    {
        InitSetting();
        FindPlayer();
        pool = GetComponent<ObjectPool>();
    }

    private void Update()
    {
        IsReach();

        Quaternion lookTarget = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Euler(0, lookTarget.eulerAngles.y, 0);

        if (distance <= 15f && canAtk)
        {
            canAtk = false;
            StartCoroutine(Attack());
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
