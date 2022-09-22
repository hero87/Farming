using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.AI;
using System.Threading.Tasks;
using System;
using Unity.VisualScripting;

public enum EnemyState { Patrolling, Attacking }

public class EnemyAI : MonoBehaviour
{

    [SerializeField] private float patrollingRange;

    [SerializeField] private NavMeshAgent navAgent;
    [SerializeField] private Animator animator;
    private AnimalAI animalAI;
    private float animalDestroyTime=1.2f;
    

    public EnemyState enemyState { get; private set; }


    private void Start() => enemyState = EnemyState.Patrolling;

    private void Update()
    {
        switch (enemyState)
        {
            case EnemyState.Patrolling:
                Patrolling();
                break;

            case EnemyState.Attacking:
                Attacking();
                break;
        }
    }

  
    private void Patrolling()
    {
        navAgent.isStopped = false;
        animator.SetFloat("Move", navAgent.velocity.magnitude);
        navAgent.speed = 5;

        if (navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            if (Extensions.GetRandomPoint(transform.position, patrollingRange, out Vector3 point))
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                navAgent.SetDestination(point);
            }
        }
    }

    private void Attacking()
    {
        navAgent.isStopped = true;
        navAgent.speed = 0;
        animator.SetFloat("Move", -1);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out AnimalAI animalAi))
        {
            enemyState = EnemyState.Attacking;
            StartCoroutine(MoveAfterAttack());
            animalAi.GetComponent<NavMeshAgent>().enabled = false;
            animalAi.GetComponentInChildren<Animator>().SetBool("Death", true);

            Destroy(animalAi.gameObject,animalDestroyTime);
        }
    }

    private IEnumerator MoveAfterAttack()
    {
        yield return new WaitForSeconds(0.5f);
        enemyState = EnemyState.Patrolling;
    }




}
