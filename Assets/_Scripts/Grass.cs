using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    private bool isTakenByAnimal;

    private void Awake() => transform.rotation = Extensions.GetRandomRotation();

    // TODO Solve OnTriggerEnter problem using lists or  OnTriggerStay ?
    // TODO add hunger time to each animal
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out AnimalAI animalsAi) && animalsAi.animalState == AnimalState.Patrolling && !isTakenByAnimal)
        {
            animalsAi.SetGrassTarget(this);
            isTakenByAnimal = true;
        }
    }
}
