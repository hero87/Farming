using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    private bool isTakenByAnimal;

    private void Awake() => transform.rotation = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up);

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out AnimalsAi animalsAi) && !isTakenByAnimal)
        {
            animalsAi.SetGrassTarget(this);
            isTakenByAnimal = true;
        }
    }
}
