using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG;
using DG.Tweening;

public class LevelManager : MonoBehaviour
{
    [Header("Levels List")]
    [SerializeField] private Level[] levels;

    [Header("Animals")]
    // TODO load prefabs from resources
    [SerializeField] private GameObject chickenPrefab;
    [SerializeField] private GameObject sheepPrefab;
    [SerializeField] private GameObject cowPrefab;

    [Header("Buildings")]
    [SerializeField] private GameObject cakeBakery;
    [SerializeField] private GameObject breadBakery;
    [SerializeField] private GameObject meatFactory;
    //[SerializeField] private GameObject milkFactory;
    [SerializeField] private GameObject burgerResturant;

    [Header("Instantiation Points")]
    [SerializeField] private float instantiationRange;
    [SerializeField] private Transform chickensParent;
    [SerializeField] private Transform sheepsParent;
    [SerializeField] private Transform cowsParent;

   


  

    public int CurrentLevelNumber => PlayerPrefs.GetInt("CURRENT_LEVEL_NUMBER");
    public Level CurrentLevel => levels[CurrentLevelNumber];


    private int currentCoinsCount;
    public int CurrentCoinsCount
    {
        set { currentCoinsCount = value;  }
        get => currentCoinsCount;
    }

   

    public static LevelManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else throw new Exception("There is already a LevelManager object!");

    }

    private void Update() => CheckLevelProgress();

    private void Start()
    {
        CurrentLevel.Initiate();

        CurrentCoinsCount = CurrentLevel.GetSetting(Settings.Key.CoinsCount);
        Well.Instance.Capacity = CurrentLevel.GetSetting(Settings.Key.WellCapacity);

        //milkFactory.SetActive(CurrentLevel.Contains(Mission.Key.MilkCount));
        meatFactory.SetActive(CurrentLevel.Contains(Mission.Key.MeatsCount));
        cakeBakery.SetActive(CurrentLevel.Contains(Mission.Key.CakesCount));
        breadBakery.SetActive(CurrentLevel.Contains(Mission.Key.BreadsCount));
        burgerResturant.SetActive(CurrentLevel.Contains(Mission.Key.BurgersCount));
    }

    private void InstantiateAnimal(GameObject animal, Transform instantiationPoint)
    {
        if (Extensions.GetRandomPoint(instantiationPoint.position, instantiationRange, out var position))
        {
            var rotation = Extensions.GetRandomRotation();
            Instantiate(animal, position, rotation, instantiationPoint);
            return;
        }

        throw new Exception($"ERROR | Cannot Instantiate {animal.name}!");
    }

    private void CheckLevelProgress()
    {
        if (CurrentLevel.Completed)
        {
            if (Time.time <= CurrentLevel.GetSetting(Settings.Key.GoldTime))
            {
                Time.timeScale = 0.0f;
                throw new Exception("Gold Time Winning");
            }
            else
            {
                Time.timeScale = 0.0f;
                throw new Exception("Normal Time Wining");
            }
        }

        if (Time.time >= CurrentLevel.GetSetting(Settings.Key.MaximumTime))
        {
            Time.timeScale = 0.0f;
            throw new Exception("Game Over");
        }
    }


    #region Events
    public void InstantiateNewChicken()
    {
        var price = CurrentLevel.GetSetting(Settings.Key.ChicknPrice);

        if (CurrentCoinsCount < price) return;
        CurrentCoinsCount -= price;
        InstantiateAnimal(chickenPrefab, chickensParent);

        try { CurrentLevel.AddProgressTo(Mission.Key.ChickensCount); }
        catch (Exception exception) { if (!CurrentLevel.Contains(Mission.Key.EggsCount)) throw exception; }
    }

    public void InstantiateNewCow()
    {
        var price = CurrentLevel.GetSetting(Settings.Key.CowPrice);

        if (CurrentCoinsCount < price) return;
        CurrentCoinsCount -= price;
        InstantiateAnimal(cowPrefab, cowsParent);

        try { CurrentLevel.AddProgressTo(Mission.Key.CowsCount); }
        catch (Exception exception) { if (!CurrentLevel.Contains(Mission.Key.MilksCount)) throw exception; }
    }

    public void InstantiateNewSheep()
    {
        var price = CurrentLevel.GetSetting(Settings.Key.SheepPrice);

        if (CurrentCoinsCount < price) return;
        CurrentCoinsCount -= price;
        InstantiateAnimal(sheepPrefab, sheepsParent);

        try { CurrentLevel.AddProgressTo(Mission.Key.SheepsCount); }
        catch (Exception exception) { if (!CurrentLevel.Contains(Mission.Key.MeatsCount)) throw exception; }
    }

    public void FillWell()
    {
        var fillPrice = CurrentLevel.GetSetting(Settings.Key.WellFillPrice);
        if (CurrentCoinsCount >= fillPrice && Well.Instance.Fill())
            CurrentCoinsCount -= fillPrice;
    }

   
    #endregion
}
