using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AnimalsAi : MonoBehaviour
{
    public enum State { patrolling, chasing, eating, creating }
    private State state;
    private bool isCreated=false;
    private float animalSpeed;
    private float distance;

    [SerializeField] private Chicken chicken;
    [SerializeField] private NavMeshAgent animalNav;
    [SerializeField] private float range;
    [SerializeField] private Transform TestPos;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform animal;
    [SerializeField] private float chasingRange = 5;
    [SerializeField] private float eatingRange = 0.5f;


    private Grass currentGrass = null;
    
    
    void Start()
    {
        animalNav = GetComponent<NavMeshAgent>();
        chicken = chicken.GetComponent<Chicken>();
    }


    void Update()
    {
    //  distance = Vector3.Distance(grass.position, animal.transform.position);
        animator.SetFloat("Move", animalSpeed);


        Patrolling();
        Chasing();
        Eating();
        Creating();

    }



    public void SetState(State state)
    {
        this.state = state;
    }




    private void Patrolling()
    {

        if (currentGrass != null) { return; }

        {
            SetState(State.patrolling);
            if (animalNav.remainingDistance <= animalNav.stoppingDistance)
            {
                Vector3 point;
                if (RandomPoint(TestPos.position, range, out point))
                {
                    Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                    animalNav.SetDestination(point);
                }
            }
        }
            animalNav.speed = 1;
            animalSpeed = animalNav.velocity.magnitude;
       
    }





    private void Chasing()
    {
        if (distance < chasingRange)
        {
            SetState(State.chasing);
         //   animalNav.SetDestination(grass[0 & 1 & 2 & 3 & 4 & 5].transform.position);
            animalNav.speed = 5;
            animalSpeed = animalNav.velocity.magnitude;
        }
    }






    private void Eating()
    {
        if (distance < eatingRange)
        {
            SetState(State.eating);
            animalSpeed = -1;
        }
    }





    private void Creating()
    {
        if (state == State.eating)
        {
            SetState(State.creating);
            if (isCreated == false)
            {
                StartCoroutine(chicken.Instant());
                isCreated = true;
            }
        }
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
