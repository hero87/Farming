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
    [SerializeField] private float lifTime;
    [SerializeField] private float productImpulse;
    [SerializeField] private float patrollingRange;
    [SerializeField] private Image healthBar;
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent navAgent;
    [SerializeField] private GameObject productPrefab;
    [SerializeField] private Transform productInstantiationPoint;

    private Grass currentGrass;
    private float currentLifeLime;

    public AnimalState State { get; private set; }


    private void Awake()
    {
        State = AnimalState.Patrolling;
        currentLifeLime = lifTime;
    }

    private void Update()
    {
        ManageState();
        ManageLifeTime();
    }

    private void ManageState()
    {
        switch (State)
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
        if (State == AnimalState.Patrolling || State == AnimalState.Chasing) currentLifeLime -= Time.deltaTime;
        else currentLifeLime = lifTime;

        if (currentLifeLime <= 0) Destroy(gameObject);

        healthBar.fillAmount = currentLifeLime/lifTime;
    }

    public void SetGrassTarget(Grass grass)
    {
        currentGrass = grass;
        State = AnimalState.Chasing;
    }

    #region States

    private void Patrolling()
    {
        navAgent.isStopped = false;
        animator.SetFloat("Move", navAgent.velocity.magnitude);
        navAgent.speed = 2;

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
            State = AnimalState.Eating;
            Eating();
        }
    }

    private async void Eating()
    {
        animator.SetFloat("Move", -1);
        await Task.Delay(7000);
        Destroy(currentGrass.gameObject);
        currentGrass = null;
        State = AnimalState.Creating;
    }

    private void Creating()
    {
        State = AnimalState.Patrolling;
        var product = Instantiate(productPrefab, productInstantiationPoint.position, Quaternion.identity);
        product.GetComponent<Rigidbody>().AddForce(productInstantiationPoint.up * productImpulse);
    }

    #endregion
}
