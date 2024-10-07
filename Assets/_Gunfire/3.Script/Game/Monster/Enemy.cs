using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class EnemyDropItem
{
    public ItemSO itemData;
    public float percentage;
    public int maxDropCount;
}

public abstract class Enemy : MonoBehaviour, IHitable
{
    public MonsterSO MonsterData;
    protected Animator animator;
    protected NavMeshAgent agent;

    protected Collider cd;
    protected Rigidbody rigid;
    protected List<Transform> players;
    [HideInInspector]
    public Transform target;
    HeadShot headshot;

    [HideInInspector]
    public int hp;
    protected int shield;
    protected bool isDead = false;

    protected float distance; // 이거 지우기/? 리턴을로 값을 받음
    protected Coroutine move;
    protected LayerMask targetLayer;

    [SerializeField]
    protected List<EnemyDropItem> dropItems;

    protected void Awake()
    {
        cd = GetComponent<Collider>();
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        headshot = GetComponent<HeadShot>();
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
    public virtual float IsReach()
    {
        distance = Vector3.Distance(transform.position, target.position);
        return distance;
    }

    public abstract IEnumerator Attack();

    public virtual IEnumerator Move()
    {
        float time = 0;
        int cool = Random.Range(1, 3);
        animator.SetBool("Walk", true);

        while (time < cool)
        {
            rigid.velocity = transform.forward * 3f;
            time += Time.deltaTime;
            yield return null;
        }

        rigid.velocity = Vector3.zero;
        animator.SetBool("Walk", false);
        move = null;
    }

    public virtual void Hit(int damage)
    {
        if(headshot.headShot)
        {
            damage = (int)(damage * 1.5f);
            hp -= (int)(damage * 1.5f);

            GameUIManager.instance.HeadShotUI();
        }
        else
        {
            hp -= damage;
        }

        if(GameManager.Instance.maxDamage < damage)
        {
            GameManager.Instance.maxDamage = damage;
        }
        
        print($"{name} 피 닳음 -> -{damage} : 잔여hp -> {hp}");

        if(!isDead && hp <= 0)
        {
            hp = 0;
            isDead = true;
            //agent.speed = 0f;
            animator.SetTrigger("Dead");
            DropItem();
            StartCoroutine(Dead());
            GameManager.Instance.enemyCount += 1;
        }
    }

    public IEnumerator Dead()
    {
        rigid.velocity = Vector3.zero;
        rigid.useGravity = false;
        cd.isTrigger = true;
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }

    public virtual void DropItem()
    {
        ItemDropManager.Instance.DropItemOnMonster(dropItems, transform);
    }
}
