using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleRat : Monster
{
    bool reack = false;

    private void Start()
    {
        
    }

    private void OnEnable()
    {
        InitSetting();
        FindPlayer();
        print($"MoleRat Target => {target}");
    }

    private void Update()
    {
        //if(!reack)
        //{
        //    //transform.LookAt(/*가까운 플레이어*/);
        //    Vector3 targetPosition = transform.position + Vector3.forward * MonsterData.speed * Time.deltaTime;
        //    rigid.MovePosition(targetPosition);
        //}
    }

    public override void Attack()
    {
        
    }

    public override void Move()
    {
        
    }

}
