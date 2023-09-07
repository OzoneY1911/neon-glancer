using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySoldierAI : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform player;
    [SerializeField] LayerMask isGround, isPlayer;

    public static float attackRange;
    bool playerInAttackRange;

    bool canChase = true;
    bool canAttack = true;

    void Awake()
    {
        attackRange = 9f;

        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, isPlayer);

        if (!playerInAttackRange)
        {
            ChasePlayer();
        }
        else
        {
            AttackPlayer();
        }
    }

    void ChasePlayer()
    {
        transform.LookAt(null);

        if (canChase)
        {
            StartCoroutine(DelayDestination(player.position, 3f, canChase));
        }
    }

    void AttackPlayer()
    {
        transform.LookAt(player);

        RaycastHit hit;
        if (Physics.Raycast(GetComponent<EnemyShooting>().projectileOrigin.position, transform.forward, out hit,  (int)attackRange))
        {
            if (hit.collider.tag == "Player")
            {
                if (canAttack)
                {
                    StartCoroutine(DelayDestination(transform.position, 3f, canAttack));
                }

                GetComponent<EnemyShooting>().EnemyShoot();
            }
            else
            {
                ChasePlayer();
            }
        }
    }

    IEnumerator DelayDestination(Vector3 destination, float delay, bool trigger)
    {
        trigger = false;

        agent.SetDestination(destination);

        yield return new WaitForSeconds(delay);

        trigger = true;
    }
}
