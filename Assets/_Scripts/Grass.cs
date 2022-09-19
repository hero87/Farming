using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    private bool isTakenByAnimal;

    private void Awake() => transform.rotation = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up);

    // TODO Solve OnTriggerEnter problem using lists or  OnTriggerStay ?
    // TODO add hunger time to each animal
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out AnimalsAi animalsAi) && animalsAi.animalState == AnimalState.Patrolling && !isTakenByAnimal)
        {
            animalsAi.SetGrassTarget(this);
            isTakenByAnimal = true;
        }
    }
}
