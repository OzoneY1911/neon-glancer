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

    void Awake()
    {
        attackRange = 12f;

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
        agent.SetDestination(player.position);
    }

    void AttackPlayer()
    {
        transform.LookAt(player);

        RaycastHit hit;
        if (Physics.Raycast(GetComponent<EnemyShooting>().projectileOrigin.position, transform.forward, out hit,  (int)attackRange))
        {
            if (hit.collider.tag == "Player")
            {
                agent.SetDestination(transform.position);

                GetComponent<EnemyShooting>().EnemyShoot();
            }
            else
            {
                ChasePlayer();
            }
        }
    }
}
