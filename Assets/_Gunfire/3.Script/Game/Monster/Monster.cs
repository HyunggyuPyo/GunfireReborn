using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour, IHitable
{
    public MonsterSO MonsterData;

    protected Rigidbody rigid;
    protected List<Transform> players;
    protected Transform target;

    protected int hp;
    protected int shield;

    protected void Awake()
    {
        rigid = GetComponent<Rigidbody>();

        hp = MonsterData.maxHealth;
        shield = MonsterData.maxShield;
        players = new List<Transform>();
    }

    public virtual void InitSetting()
    {
        GameObject playersPos = GameObject.Find("Players");

        if(playersPos != null)
        {
            foreach (Transform playerPos in playersPos.transform)
            {
                players.Add(playerPos);
            }
        }
        
    }

    public virtual void FindPlayer()
    {
        Transform targetPlayer = null;
        float closesDistance = Mathf.Infinity;

        foreach(Transform player in players)
        {
            float distance = Vector3.Distance(transform.position, player.position);

            if(distance < closesDistance)
            {
                closesDistance = distance;
                targetPlayer = player;
            }
        }

        target = targetPlayer;
    }

    public abstract void Attack();

    public abstract void Move();

    public virtual void Hit(int damage)
    {
        
    }
}
