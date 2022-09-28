using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class Enemy : NPC
{
    public enum EnemyState { Patrolling, Attacking }
    public EnemyState State { get; private set; }


    public override int MaxHealth => LevelManager.Instance.GetSetting(Settings.Key.EnemyMaxHealth);
    public override int PatrollingRange => LevelManager.Instance.GetSetting(Settings.Key.EnemyPatrollingRange);
    public override int PatrollingSpeed => LevelManager.Instance.GetSetting(Settings.Key.EnemyPatrollingSpeed);


    #region Unity Callbacks
    protected void Start()
    {
        State = EnemyState.Patrolling;
        LevelManager.Instance.NumberOfActiveEnemies++;
    }

    protected void Update() { if (IsDead) return; ManageStates(); }

    protected void OnDestroy() => LevelManager.Instance.NumberOfActiveEnemies--;

    protected void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Animal animal))
        {
            Attacking();
            animal.GetComponentInChildren<Animator>().SetBool("Death", true);
            animal.CurrentHealth = 0;
        }
    }
    #endregion


    #region Other Callbacks

    protected override void ManageStates()
    {
        switch (State)
        {
            case EnemyState.Patrolling:
                Patrolling();
                break;
        }
    }

    protected async void Attacking()
    {
        State = EnemyState.Attacking;

        navAgent.speed = 0;
        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;
        animator.SetFloat("Move", -1);

        await Task.Delay(400); 
        State = EnemyState.Patrolling;
    }

    #endregion
}
