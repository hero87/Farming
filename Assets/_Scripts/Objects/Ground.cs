using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private Grass grassPrefab;
    [SerializeField] private Transform grassParent;


    public static Ground Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else throw new System.Exception("There is already a Ground object!");
    }

    public void PlantGrass(Vector3 position)
    {
        if (Well.Instance.IsFillingWater) return;
        if (!Well.Instance.UseWater()) return;
        Instantiate(grassPrefab, position, Quaternion.identity, grassParent);
    }
}
