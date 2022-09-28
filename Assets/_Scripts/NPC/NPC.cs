using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public abstract class NPC : MonoBehaviour
{
    public abstract int MaxHealth { get; }
    public abstract int PatrollingRange { get; }
    public abstract int PatrollingSpeed { get; }


    public bool IsDead { get; protected set; }


    protected HealthBar healthBar;
    protected NavMeshAgent navAgent;
    protected Animator animator;


    private int currentHealth;
    public int CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            currentHealth = value;
            var percentage = (float)currentHealth / MaxHealth;
            healthBar.SetValue(percentage);
            if (currentHealth <= 0)
            {
                IsDead = true;
                Die();
            }
        }
    }

    protected void Awake()
    {
        currentHealth = MaxHealth;
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        healthBar = GetComponentInChildren<HealthBar>();
    }


    protected abstract void ManageStates();


    protected void Die()
    {
        navAgent.speed = 0;
        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;
        animator.SetBool("Death", true);

        Destroy(navAgent);
        GetComponents<Collider>().ToList().ForEach(c => Destroy(c));

        Destroy(healthBar.gameObject);
        Destroy(gameObject, 2.5f);
    }

    protected void Patrolling()
    {
        navAgent.isStopped = false;
        navAgent.speed = PatrollingSpeed;
        animator.SetFloat("Move", navAgent.velocity.magnitude);

        if (navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            if (Extensions.GetRandomPoint(transform.position, PatrollingRange, out Vector3 point))
            {
                // Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                navAgent.SetDestination(point);
            }
        }
    }
}
