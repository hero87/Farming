using UnityEngine;
using System.Threading.Tasks;


public abstract class Animal : NPC
{
    public enum AnimalState { Patrolling, Chasing, Eating, Creating }
    public AnimalState State { get; private set; }


    [SerializeField] private GameObject productPrefab;
    [SerializeField] private Transform productInstantiationPoint;

    private Grass currentGrass;
    private int currentHungerTime;

    public abstract int EatingTime { get; }
    public abstract int HungerTime { get; }
    public abstract int ChasingSpeed { get; }



    #region Unity Callbacks

    protected void Start()
    {
        State = AnimalState.Patrolling;
        currentHungerTime = HungerTime;
    }

    protected void Update()
    {
        if (IsDead) return;
        ManageStates();
        ManageTime();
    }

    #endregion


    protected override void ManageStates()
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

    private void ManageTime()
    {
        if (State != AnimalState.Patrolling)
        {
            CurrentHealth = MaxHealth;
            currentHungerTime = HungerTime;
            return;
        } // else if patrolling

        if (currentHungerTime > 0)
        {
            currentHungerTime -= (int)(1000 * Time.deltaTime);
        }
        else
        {
            CurrentHealth -= (int)(1000 * Time.deltaTime);
        }
    }

    public bool StartChasing(Grass grass)
    {
        if (currentHungerTime > 0) return false;
        if (currentGrass != null) return false;

        currentGrass = grass;
        State = AnimalState.Chasing;
        return true;
    }



    #region States

    private void Chasing()
    {
        navAgent.speed = ChasingSpeed;
        navAgent.SetDestination(currentGrass.transform.position);
        animator.SetFloat("Move", navAgent.velocity.magnitude);

        if (!navAgent.pathPending && navAgent.remainingDistance <= navAgent.stoppingDistance)
            Eating();
    }

    private async void Eating()
    {
        navAgent.speed = 0;
        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;

        State = AnimalState.Eating;

        animator.SetFloat("Move", -1);

        await Task.Delay(EatingTime);

        Destroy(currentGrass.gameObject);
        currentGrass = null;

        State = AnimalState.Creating;
    }

    private void Creating()
    {
        Instantiate(productPrefab, productInstantiationPoint.position, Quaternion.identity);
        State = AnimalState.Patrolling;
    }

    #endregion
}
