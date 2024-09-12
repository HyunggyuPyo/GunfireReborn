using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour, IHitable
{
    public MonsterSO MonsterData;
    protected Animator animator;

    protected Rigidbody rigid;
    protected List<Transform> players;
    protected Transform target;

    protected int hp;
    protected int shield;

    protected float distance;

    protected LayerMask targetLayer;

    protected void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        targetLayer = (1 << LayerMask.NameToLayer("Player"));

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
    public virtual void IsReach()
    {
        distance = Vector3.Distance(transform.position, target.position);
    }

    public abstract IEnumerator Attack();

    public abstract void Move();

    public virtual void Hit(int damage)
    {
        
    }
}