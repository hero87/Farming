using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.AI;
using System.Threading.Tasks;
using System;
using UnityEngine.UI;

//public enum EnemyState { Patrolling, Chasing, Attaking }


public class EnemyAI : MonoBehaviour
{

    [SerializeField] private float patrollingRange;

    [SerializeField] private NavMeshAgent navAgent;
    [SerializeField] private Animator animator;

    private AnimalAI targetAnimal;


    //public EnemyState enemyState { get; private set; }


    //private void Start() => enemyState = EnemyState.Patrolling;

    private void Update()
    {

                Patrolling();

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out AnimalAI animalAi))
        {
            navAgent.isStopped = true;
            animator.SetFloat("Move", -1);
            Destroy(animalAi.gameObject);

        }
    }




}
