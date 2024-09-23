using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour, IHitable
{
    public MonsterSO MonsterData;
    protected Animator animator;
    protected NavMeshAgent agent;

    protected Rigidbody rigid;
    protected List<Transform> players;
    [HideInInspector]
    public Transform target;

    [HideInInspector]
    public int hp;
    protected int shield;
    protected bool isDead = false;

    protected float distance;

    protected LayerMask targetLayer;

    protected void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
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
        hp -= damage;
        print($"{name} ÇÇ ´âÀ½ -> -{damage} : ÀÜ¿©hp -> {hp}");

        if(hp <= 0)
        {
            hp = 0;
            isDead = true;
            //agent.speed = 0f;
            animator.SetTrigger("Dead");
            StartCoroutine(Dead());
        }
    }

    public IEnumerator Dead()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
