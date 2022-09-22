using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [Header("Levels List")]
    [SerializeField] private Level[] levels;

    [Header("Animals")]
    [SerializeField] private GameObject chickenPrefab;
    [SerializeField] private GameObject sheepPrefab;
    [SerializeField] private GameObject cowPrefab;

    [Header("Buildings")]
    [SerializeField] private GameObject cakeBakery;
    [SerializeField] private GameObject breadBakery;
    [SerializeField] private GameObject meatFactory;
    [SerializeField] private GameObject milkFactory;
    [SerializeField] private GameObject burgerResturant;

    [Header("Instantiation Points")]
    [SerializeField] private float instantiationRange;
    [SerializeField] private Transform chickensParent;
    [SerializeField] private Transform sheepsParent;
    [SerializeField] private Transform cowsParent;

    [Header("GUI")]
    [SerializeField] private TextMeshProUGUI coinsCountText;
    [SerializeField] private Button creatNewChickenButton;
    [SerializeField] private Button creatNewCowButton;
    [SerializeField] private Button creatNewSheepButton;


    public int CurrentLevelNumber => PlayerPrefs.GetInt("CURRENT_LEVEL_NUMBER");
    public Level CurrentLevel => levels[CurrentLevelNumber];


    private int currentCoinsCount;
    public int CurrentCoinsCount
    {
        set { currentCoinsCount = value; coinsCountText.text = $"{currentCoinsCount}"; }
        get => currentCoinsCount;
    }

    public static LevelManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else throw new Exception("There is already a LevelManager object!");
        InitiateLevel();
    }

    private void Update() => CheckLevelProgress();

    private void InitiateLevel()
    {
        CurrentCoinsCount = CurrentLevel.GetSetting(Settings.Key.CoinsCount);
        Well.Instance.Capacity = CurrentLevel.GetSetting(Settings.Key.WellCapacity);

        milkFactory.SetActive(CurrentLevel.Contains(Mission.Key.MilkCount));
        meatFactory.SetActive(CurrentLevel.Contains(Mission.Key.MeatCount));
        cakeBakery.SetActive(CurrentLevel.Contains(Mission.Key.CakeCount));
        breadBakery.SetActive(CurrentLevel.Contains(Mission.Key.BreadCount));
        burgerResturant.SetActive(CurrentLevel.Contains(Mission.Key.BurgerCount));

        var activateCowButton = CurrentLevel.Contains(Mission.Key.CowsCount) | CurrentLevel.Contains(Mission.Key.MilkCount);
        var activateSheepButton = CurrentLevel.Contains(Mission.Key.SheepsCount) | CurrentLevel.Contains(Mission.Key.MeatCount);
        var activateChicknButton = CurrentLevel.Contains(Mission.Key.ChickensCount) | CurrentLevel.Contains(Mission.Key.EggsCount);

        creatNewCowButton.gameObject.SetActive(activateCowButton);
        creatNewSheepButton.gameObject.SetActive(activateSheepButton);
        creatNewChickenButton.gameObject.SetActive(activateChicknButton);
    }

    public void CreateNewChicken()
    {
        var price = CurrentLevel.GetSetting(Settings.Key.ChicknPrice);

        if (CurrentCoinsCount < price) return;
        CurrentCoinsCount -= price;
        InstantiateAnimal(chickenPrefab, chickensParent);

        CurrentLevel.AddProgressTo(Mission.Key.ChickensCount);
        try { CurrentLevel.AddProgressTo(Mission.Key.ChickensCount); }
        catch (Exception exception) { if (!CurrentLevel.Contains(Mission.Key.EggsCount)) throw exception; }
    }

    public void CreateNewCow()
    {
        var price = CurrentLevel.GetSetting(Settings.Key.CowPrice);

        if (CurrentCoinsCount < price) return;
        CurrentCoinsCount -= price;
        InstantiateAnimal(cowPrefab, cowsParent);

        try { CurrentLevel.AddProgressTo(Mission.Key.CowsCount); }
        catch (Exception exception) { if (!CurrentLevel.Contains(Mission.Key.MilkCount)) throw exception; }
    }

    public void CreateNewSheep()
    {
        var price = CurrentLevel.GetSetting(Settings.Key.SheepPrice);

        if (CurrentCoinsCount < price) return;
        CurrentCoinsCount -= price;
        InstantiateAnimal(sheepPrefab, sheepsParent);

        try { CurrentLevel.AddProgressTo(Mission.Key.SheepsCount); }
        catch (Exception exception) { if (!CurrentLevel.Contains(Mission.Key.MeatCount)) throw exception; }
    }

    public void FillWell()
    {
        var fillPrice = CurrentLevel.GetSetting(Settings.Key.WellFillPrice);
        if (CurrentCoinsCount >= fillPrice && Well.Instance.Fill())
            CurrentCoinsCount -= fillPrice;
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
                throw new Exception("Gold Time Winning");
            else
                throw new Exception("Normal Time Wining");
        }

        if (Time.time >= CurrentLevel.GetSetting(Settings.Key.MaximumTime))
            throw new Exception("Game Over");
    }

    private void OnDestroy() => CurrentLevel.Reset();
}
