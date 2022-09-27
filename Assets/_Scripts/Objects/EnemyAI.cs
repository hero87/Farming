using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public enum EnemyState { Patrolling, Attacking }

public class EnemyAI : MonoBehaviour
{

    [SerializeField] private float patrollingRange;

    [SerializeField] private NavMeshAgent navAgent;
    [SerializeField] private Animator animator;

    [SerializeField] private int maxHealth;
    [SerializeField] private Image healthBar;
    [SerializeField] private Gradient gradient;


    public EnemyState enemyState { get; private set; }


    private void Start()
    {
        enemyState = EnemyState.Patrolling;
        LevelManager.Instance.NumberOfActiveEnemies++;
        currentHealth = maxHealth;  
    }


    int currentHealth;
    public void AcceptDamage(int value)
    {
        currentHealth -= value;
        if (currentHealth <= 0) Destroy(gameObject);
    }
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

        //Update Health Bar
        var helthPercentage = (float)currentHealth / maxHealth;
        healthBar.fillAmount = helthPercentage;
        healthBar.color = gradient.Evaluate(helthPercentage);
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

            animalAi.KilledByEnemy = true;
        }
    }

    private IEnumerator MoveAfterAttack()
    {
        yield return new WaitForSeconds(0.5f);
        enemyState = EnemyState.Patrolling;
    }

    private void OnDestroy() => LevelManager.Instance.NumberOfActiveEnemies--;
}
