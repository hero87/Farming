using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using System.Threading.Tasks;


public enum AnimalState { Patrolling, Chasing, Eating, Creating }


public class AnimalsAi : MonoBehaviour
{
    [SerializeField] private float patrollingRange;

    [SerializeField] private NavMeshAgent navAgent;
    [SerializeField] private Animator animator;

    [SerializeField] private Transform animalsProductPrefab;


    private AnimalState animalState;
    private Grass currentGrass;


    private void Start() => animalState = AnimalState.Patrolling;


    private void Update()
    {
        switch (animalState)
        {
            case AnimalState.Patrolling:
                Patrolling();
                break;

            case AnimalState.Chasing:
                Chasing();
                break;

            case AnimalState.Creating:
                Creating();
                break;
        }
    }

    public void SetGrassTarget(Grass grass)
    {
        if (animalState != AnimalState.Patrolling) return;

        currentGrass = grass;
        animalState = AnimalState.Chasing;
    }

    private void Patrolling()
    {
        navAgent.isStopped = false;
        animator.SetFloat("Move", navAgent.velocity.magnitude);
        navAgent.speed = 3;


        //if (currentGrass != null) return;

        if (navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            if (RandomPoint(transform.position, patrollingRange, out Vector3 point))
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                navAgent.SetDestination(point);
            }
        }

        navAgent.speed = 1;
    }

    private void Chasing()
    {
        navAgent.SetDestination(currentGrass.transform.position);
        navAgent.speed = 5;
        animator.SetFloat("Move", navAgent.velocity.magnitude);

        if (!navAgent.pathPending && navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            navAgent.isStopped = true;
            navAgent.velocity = Vector3.zero;
            animalState = AnimalState.Eating;
            Eating();
        }
    }

    private async void Eating()
    {
        animator.SetFloat("Move", -1);
        await Task.Delay(7000);
        Destroy(currentGrass.gameObject);
        currentGrass = null;
        animalState = AnimalState.Creating;
    }

    private void Creating()
    {
        animalState = AnimalState.Patrolling;
        Instantiate(animalsProductPrefab, transform.position, Quaternion.identity);
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}
