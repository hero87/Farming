using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    [SerializeField] private GameObject egg;
    [SerializeField] private Transform instantPosition;

    public IEnumerator Instant()
    {
        yield return new WaitForSeconds(2);
        Instantiate(egg, instantPosition.position, Quaternion.identity);
    }
}
