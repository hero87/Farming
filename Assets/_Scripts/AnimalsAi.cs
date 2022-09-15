using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using System.Threading.Tasks;

public class AnimalsAi : MonoBehaviour
{
    public enum State { patrolling, chasing, eating, creating }
    private State state;
    private float animalSpeed;

    [SerializeField] private NavMeshAgent animalNav;
    [SerializeField] private float range;
    [SerializeField] private Transform TestPos;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform animal;
    [SerializeField] private Transform eggPrefab;

    private Grass currentGrass = null;




    void Start()
    {
        animalNav = GetComponent<NavMeshAgent>();
       

        SetState(State.patrolling);
    }


    void Update()
    {



        animator.SetFloat("Move", animalSpeed);


        switch (state)
        {
            case State.patrolling:
                Patrolling();
                break;
            case State.chasing:
                Chasing();
                break;
          
            case State.creating:
                Creating();
                break;
            default:
                break;
        }


    }



    public void SetState(State state)
    {
        this.state = state;
    }

    internal void SetGrassTarget(Grass grass)
    {
        if (state != State.patrolling) { return; }
        currentGrass = grass;
        SetState(State.chasing);
    }




    private void Patrolling()
    {


        if (currentGrass != null) { return; }

        if (animalNav.remainingDistance <= animalNav.stoppingDistance)
        {
            Vector3 point;
            if (RandomPoint(TestPos.position, range, out point))
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                animalNav.SetDestination(point);
            }
        }
        animalNav.speed = 1;
        animalSpeed = animalNav.velocity.magnitude;

    }





    private void Chasing()
    {


        animalNav.SetDestination(currentGrass.transform.position); // add grassIndex New
        animalNav.speed = 5;
        animalSpeed = animalNav.velocity.magnitude;

        if (!animalNav.pathPending && animalNav.remainingDistance <= animalNav.stoppingDistance)
        {
            SetState(State.eating);
            Eating();
        }


    }






    private async void Eating()
    {
        animalSpeed = -1;

       

        await Task.Delay(7000);
        currentGrass.gameObject.SetActive(false);
        currentGrass = null;

        SetState(State.creating);




    }


   

    private void Creating()
    {
        SetState(State.patrolling);
        var egg = Instantiate(eggPrefab, transform.position, Quaternion.identity);

       


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
