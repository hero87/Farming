using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ChickenControl : MonoBehaviour
{
    public Camera MyCamera;
    public LayerMask Ground;
    NavMeshAgent Chicken;
    Animator animator;

    private void Start()
    {
        Chicken = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoveTo();
        }
        animator.SetFloat("Move", Chicken.velocity.magnitude);
    }

    private void MoveTo()
    {
        RaycastHit hit;
        Ray ray = MyCamera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, Ground))
        {
            Chicken.SetDestination(hit.point);
        }
    }
}
