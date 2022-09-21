using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Levels List")]
    [SerializeField] private Level[] levels;


    [Header("Animals")]
    [SerializeField] private Transform animalsParaent;
    [SerializeField] private GameObject chickenPrefab;
    [SerializeField] private GameObject sheepPrefab;
    [SerializeField] private GameObject cowPrefab;
    

    [Header("Buildings")]
    [SerializeField] private Transform buildingsParent;
    [SerializeField] private GameObject milkAndMeatFactoryPrefab;
    [SerializeField] private GameObject burgerResturantPrefab;
    [SerializeField] private GameObject cakeBakeryPrefab;
    [SerializeField] private GameObject breadBakeryPrefab;


    [Header("Instantiation Points")]
    [SerializeField] private float animalsInstantiationRange;
    [SerializeField] private Transform chickenInstantiationPoint;
    [SerializeField] private Transform sheepInstantiationPoint;
    [SerializeField] private Transform cowInstantiationPoint;
    [SerializeField] private GameObject milkAndMeatFactoryInstantiationPoint;
    [SerializeField] private GameObject burgerResturantInstantiationPoint;
    [SerializeField] private GameObject cakeBakeryInstantiationPoint;
    [SerializeField] private GameObject breadBakeryInstantiationPoint;


    public int CurrentLevelNumber => PlayerPrefs.GetInt("CURRENT_LEVEL_NUMBER");
    public Level CurrentLevelData => levels[CurrentLevelNumber];


    private void Awake() => InitiateLevel();


    private void InitiateLevel()
    {
        throw new NotImplementedException();
    }

    private void CheckLevelState()
    {
        throw new NotImplementedException();
    }

    private void InstantiateAnimal(GameObject animal)
    {
        if (Extensions.GetRandomPoint(chickenInstantiationPoint.position, animalsInstantiationRange, out var position))
        {
            var rotation = Extensions.GetRandomRotation();
            Instantiate(animal, position, rotation, animalsParaent);
            return;
        }

        throw new Exception($"ERROR | Cannot Instantiate {animal.name}!");
    }
}
