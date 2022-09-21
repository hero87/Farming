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
    [SerializeField] private Transform buildingsParent;
    [SerializeField] private GameObject milkAndMeatFactory;
    [SerializeField] private GameObject burgerResturant;
    [SerializeField] private GameObject cakeBakery;
    [SerializeField] private GameObject breadBakery;


    [Header("Instantiation Points")]
    [SerializeField] private float animalsInstantiationRange;
    [SerializeField] private Transform animalsParaent;
    [SerializeField] private Transform chickenInstantiationPoint;
    [SerializeField] private Transform sheepInstantiationPoint;
    [SerializeField] private Transform cowInstantiationPoint;
    //[SerializeField] private GameObject milkAndMeatFactoryInstantiationPoint;
    //[SerializeField] private GameObject burgerResturantInstantiationPoint;
    //[SerializeField] private GameObject cakeBakeryInstantiationPoint;
    //[SerializeField] private GameObject breadBakeryInstantiationPoint;


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

    private int currentEggsCount;
    private int currentMilkCount;
    private int currentMeatCount;
    private int currentBreadCount;
    private int currentCakeCount;

    private int currentChickensCount;
    private int currentCowsCount;
    private int currentSheepsCount;

    public static LevelManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else throw new Exception("There is already a LevelManager object!");
        InitiateLevel();
    }

    private void Update() => CheckLevelState();

    private void InitiateLevel()
    {
        CurrentCoinsCount = CurrentLevel.CoinsCount;
        Well.Instance.Capacity = CurrentLevel.WellCapacity;

        breadBakery.SetActive(CurrentLevel.ActivateBreadBakery);
        cakeBakery.SetActive(CurrentLevel.ActivateCakeBeakery);
        burgerResturant.SetActive(CurrentLevel.ActivateBurgerResturant);
        milkAndMeatFactory.SetActive(CurrentLevel.ActivateMilkFactory); // TODO activate scripts only
        milkAndMeatFactory.SetActive(CurrentLevel.ActivateMeatFactory); // TODO activate scripts only

        creatNewChickenButton.gameObject.SetActive(CurrentLevel.AllowChicknsCreating);
        creatNewCowButton.gameObject.SetActive(CurrentLevel.AllowCowsCreating);
        creatNewSheepButton.gameObject.SetActive(CurrentLevel.AllowSheepsCreating);
    }

    private void CheckLevelState()
    {
        if (Time.time >= CurrentLevel.MaximumTime)
            throw new Exception("more than maximum time");

        var isFinished = CurrentLevel.EggsCount <= currentEggsCount &&
                         CurrentLevel.MilkCount <= currentMilkCount &&
                         CurrentLevel.MeatCount <= currentMeatCount &&
                         CurrentLevel.BreadCount <= currentBreadCount &&
                         CurrentLevel.CakeCount <= currentCakeCount &&
                         CurrentLevel.ChickensCount <= currentChickensCount &&
                         CurrentLevel.CowsCount <= currentCowsCount &&
                         CurrentLevel.SheepsCount <= currentSheepsCount;

        var isGold = isFinished && Time.time <= CurrentLevel.GoldTime;

        if (isGold)
            throw new Exception("Gold Finished");

        if (isFinished)
            throw new Exception("Finished");
    }

    public void CreateNewChicken()
    {
        // TODO use items & animals price from the level
        if (CurrentCoinsCount < 100) return;
        CurrentCoinsCount -= 100;
        InstantiateAnimal(chickenPrefab, chickenInstantiationPoint.position);
        currentChickensCount++;
    }

    public void CreateNewCow()
    {
        // TODO use items & animals price from the level
        if (CurrentCoinsCount < 200) return;
        CurrentCoinsCount -= 200;
        InstantiateAnimal(cowPrefab, cowInstantiationPoint.position);
        currentCowsCount++;
    }

    public void CreateNewSheep()
    {
        // TODO use items & animals price from the level
        if (CurrentCoinsCount < 300) return;
        CurrentCoinsCount -= 300;
        InstantiateAnimal(sheepPrefab, sheepInstantiationPoint.position);
        currentSheepsCount++;
    }

    public void FillWell()
    {
        // TODO use well price from the level
        if (CurrentCoinsCount >= 300 && Well.Instance.Fill())
            CurrentCoinsCount -= 300;
    }

    private void InstantiateAnimal(GameObject animal, Vector3 instantiationPoint)
    {
        if (Extensions.GetRandomPoint(instantiationPoint, animalsInstantiationRange, out var position))
        {
            var rotation = Extensions.GetRandomRotation();
            Instantiate(animal, position, rotation, animalsParaent);
            return;
        }

        throw new Exception($"ERROR | Cannot Instantiate {animal.name}!");
    }
}
