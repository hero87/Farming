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
        set { currentCoinsCount = value; UIManager.Instance.UpdateCoinText(value); }
        get => currentCoinsCount;
    }


    public AnimalAI CowPrefab { get; private set; }
    public AnimalAI SheepPrefab { get; private set; }
    public AnimalAI ChickenPrefab { get; private set; }


    public static LevelManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else throw new Exception("There is already a LevelManager object!");

        CowPrefab = Resources.Load<AnimalAI>("Animals/Cow");
        SheepPrefab = Resources.Load<AnimalAI>("Animals/Sheep");
        ChickenPrefab = Resources.Load<AnimalAI>("Animals/Chicken");
    }

    private void Update() => CheckLevelProgress();

    private void Start()
    {
        CurrentLevel.Initiate();
        UIManager.Instance.Initiate();

        CurrentCoinsCount = CurrentLevel.GetSetting(Settings.Key.CoinsCount);
        Well.Instance.Capacity = CurrentLevel.GetSetting(Settings.Key.WellCapacity);

        //milkFactory.SetActive(CurrentLevel.Contains(Mission.Key.MilkCount));
        meatFactory.SetActive(CurrentLevel.Contains(TrackableType.MeatsCount));
        cakeBakery.SetActive(CurrentLevel.Contains(TrackableType.CakesCount));
        breadBakery.SetActive(CurrentLevel.Contains(TrackableType.BreadsCount));
        burgerResturant.SetActive(CurrentLevel.Contains(TrackableType.BurgersCount));
    }

    private void InstantiateAnimal(AnimalAI animal, Transform instantiationPoint)
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
        InstantiateAnimal(ChickenPrefab, chickensParent);

        try { CurrentLevel.AddProgressTo(TrackableType.ChickensCount); }
        catch (Exception exception) { if (!CurrentLevel.Contains(TrackableType.EggsCount)) throw exception; }
    }

    public void InstantiateNewCow()
    {
        var price = CurrentLevel.GetSetting(Settings.Key.CowPrice);

        if (CurrentCoinsCount < price) return;
        CurrentCoinsCount -= price;
        InstantiateAnimal(CowPrefab, cowsParent);

        try { CurrentLevel.AddProgressTo(TrackableType.CowsCount); }
        catch (Exception exception) { if (!CurrentLevel.Contains(TrackableType.MilksCount)) throw exception; }
    }

    public void InstantiateNewSheep()
    {
        var price = CurrentLevel.GetSetting(Settings.Key.SheepPrice);

        if (CurrentCoinsCount < price) return;
        CurrentCoinsCount -= price;
        InstantiateAnimal(SheepPrefab, sheepsParent);

        try { CurrentLevel.AddProgressTo(TrackableType.SheepsCount); }
        catch (Exception exception) { if (!CurrentLevel.Contains(TrackableType.MeatsCount)) throw exception; }
    }

    public void FillWell()
    {
        var fillPrice = CurrentLevel.GetSetting(Settings.Key.WellFillPrice);
        if (CurrentCoinsCount >= fillPrice && Well.Instance.Fill())
            CurrentCoinsCount -= fillPrice;
    }

    #endregion
}