using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    private bool isTakenByAnimal;

    private void Awake() => transform.rotation = Extensions.GetRandomRotation();


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out AnimalAI animalsAi) && animalsAi.State == AnimalState.Patrolling && !isTakenByAnimal)
        {
            animalsAi.SetGrassTarget(this);
            isTakenByAnimal = true;
        }
    }
}
