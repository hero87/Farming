using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalsMovement : MonoBehaviour
{


   
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent navMesh;
    [SerializeField] private Transform testPos;


    private void Start()
    {
        navMesh=GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        MoveTo(testPos.position);
        animator.SetFloat("Move", navMesh.velocity.magnitude);
    }

    private void MoveTo(Vector3 target)
    {
        navMesh.SetDestination(target);
    }



}
