using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent(out AnimalsAi animalsAi))
        {
           

            animalsAi.SetGrassTarget(this);
            
        }
    }
}
