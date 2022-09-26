using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using System.Threading.Tasks;
using UnityEngine.UI;


public enum AnimalState { Patrolling, Chasing, Eating, Creating }


public class AnimalAI : MonoBehaviour
{

    [SerializeField] private float patrollingRange;
    [SerializeField] private float lifTime;

    [SerializeField] private GameObject animalsProductPrefab;

    public Animator animator;
    public NavMeshAgent navAgent;

    public AnimalState animalState { get; private set; }
    private Grass currentGrass;

    [SerializeField] private Image healthBar;
    private float currentLifeLime;




    private void Awake()
    {
        animalState = AnimalState.Patrolling;
        currentLifeLime = lifTime;
    }


    private void Update()
    {
        ManageLifeTime();
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


    
    private void ManageLifeTime()
    {
        if (animalState == AnimalState.Patrolling || animalState == AnimalState.Chasing) currentLifeLime -= Time.deltaTime;
        else currentLifeLime = lifTime;

        if (currentLifeLime <= 0) Destroy(gameObject);

        healthBar.fillAmount = currentLifeLime/lifTime;
    }

    public void SetGrassTarget(Grass grass)
    {
        currentGrass = grass;
        animalState = AnimalState.Chasing;
    }

    private void Patrolling()
    {
        navAgent.isStopped = false;
        animator.SetFloat("Move", navAgent.velocity.magnitude);
        navAgent.speed = 2;

        //if (currentGrass != null) return;

        if (navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            if (Extensions.GetRandomPoint(transform.position, patrollingRange, out Vector3 point))
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                navAgent.SetDestination(point);
            }
        }
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


}
