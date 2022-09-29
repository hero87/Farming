using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Levels List")]
    [SerializeField] private Level[] levels;

    [Header("Buildings")]
    [SerializeField] private GameObject cakeBakery;
    [SerializeField] private GameObject breadBakery;
    [SerializeField] private GameObject meatFactory;
    [SerializeField] private GameObject burgerResturant;

    [Header("Instantiation Points")]
    [SerializeField] private float instantiationRange;
    [SerializeField] private Transform enemyParent;
    [SerializeField] private Transform chickensParent;
    [SerializeField] private Transform sheepsParent;
    [SerializeField] private Transform cowsParent;


    private int CurrentLevelNumber => PlayerPrefs.GetInt("CURRENT_LEVEL_NUMBER");
    private Level CurrentLevel => levels[CurrentLevelNumber];


    private int currentCoinsCount;
    public int CurrentCoinsCount
    {
        get => currentCoinsCount;
        set
        {
            currentCoinsCount = value;
            UIManager.Instance.UpdateCoinsCount(value);
        }
    }

    private int remainingTime;
    private int RemainingTime
    {
        get => remainingTime;
        set
        {
            remainingTime = value;

            if (CurrentLevel.IsCompleted)
            {
                var goldStep = GetSetting(SettingsKey.MaximumTime) - GetSetting(SettingsKey.GoldTime);
                if (remainingTime >= goldStep) UIManager.Instance.ViewWinPanel(remainingTime, true);
                else UIManager.Instance.ViewWinPanel(remainingTime, false);
            }
            else if (remainingTime <= 0)
            {
                remainingTime = 0;
                UIManager.Instance.ViewLosePanel();
            }
            else
            {
                UIManager.Instance.UpdateTime(remainingTime / 1000);
            }
        }
    }


    public int numberOfActiveEnemies;
    private float lastEnemyExistanceTime;


    private Animal cowPrefab;
    private Animal sheepPrefab;
    private Animal chickenPrefab;
    private Enemy enemyPrefab;

    public static LevelManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else throw new Exception("There is already a LevelManager object!");

        cowPrefab = Resources.Load<Animal>("NPCs/Cow");
        sheepPrefab = Resources.Load<Animal>("NPCs/Sheep");
        chickenPrefab = Resources.Load<Animal>("NPCs/Chicken");
        enemyPrefab = Resources.Load<Enemy>("NPCs/Dog");
    }

    private void Start()
    {
        CurrentLevel.Initiate();
        UIManager.Instance.Initiate();

        CurrentCoinsCount = GetSetting(SettingsKey.CoinsCount);
        Well.Instance.Capacity = GetSetting(SettingsKey.WellCapacity);

        cakeBakery.SetActive(ContainsMission(Objective.CakesCount));
        breadBakery.SetActive(ContainsMission(Objective.BreadsCount) || ContainsMission(Objective.CakesCount));

        burgerResturant.SetActive(ContainsMission(Objective.BurgersCount));
        meatFactory.SetActive(ContainsMission(Objective.MeatsCount) || ContainsMission(Objective.BurgersCount));

        RemainingTime = GetSetting(SettingsKey.MaximumTime);
        lastEnemyExistanceTime = GetSetting(SettingsKey.MaximumTime);
    }

    private void Update()
    {
        RemainingTime -= (int)(1000 * Time.deltaTime);

        var shouldAddNewEnemy = lastEnemyExistanceTime - remainingTime > GetSetting(SettingsKey.EnemyTime) &&
            numberOfActiveEnemies < GetSetting(SettingsKey.MaxEnemyNumber);

        if (shouldAddNewEnemy)
        {
            InstantiateObject(enemyPrefab.gameObject, enemyParent);
            lastEnemyExistanceTime = remainingTime;
        }
    }


    private void InstantiateObject(GameObject gameObject, Transform instantiationPoint)
    {
        if (Extensions.GetRandomPoint(instantiationPoint.position, instantiationRange, out var position))
        {
            var rotation = Extensions.GetRandomRotation();
            Instantiate(gameObject, position, rotation, instantiationPoint);
            return;
        }

        throw new Exception($"ERROR | Cannot Instantiate {gameObject.name}!");
    }


    // Mission and Settings Systems

    public void AddProgress(Objective key, int value) => CurrentLevel.AddProgress(key, value);
    public int GetSetting(SettingsKey key) => CurrentLevel.GetSetting(key);
    public Mission GetMission(Objective key) => CurrentLevel.GetMission(key);
    public bool ContainsMission(Objective key) => CurrentLevel.Contains(key);
    public Objective[] Objectives => CurrentLevel.Objectives;


    // Trading System

    //public void MoveFromTruckToStorage(Objective key) => Truck.Instance.MoveToStorage(key);
    //public void MoveFromStorageToTruck(Objective key) => Storage.Instance.MoveToTruck(key);
    //public void MoveAllFromTruckToStorage() => Truck.Instance.MoveAllToStorage();


    #region Buttons Events

    public void InstantiateNewChicken()
    {
        var price = GetSetting(SettingsKey.ChicknPrice);

        if (CurrentCoinsCount < price) return;
        CurrentCoinsCount -= price;
        InstantiateObject(chickenPrefab.gameObject, chickensParent);

        try { AddProgress(Objective.ChickensCount, 1); }
        catch (Exception exception) { if (!ContainsMission(Objective.EggsCount)) throw exception; }
    }

    public void InstantiateNewCow()
    {
        var price = GetSetting(SettingsKey.CowPrice);

        if (CurrentCoinsCount < price) return;
        CurrentCoinsCount -= price;
        InstantiateObject(cowPrefab.gameObject, cowsParent);

        try { AddProgress(Objective.CowsCount, 1); }
        catch (Exception exception) { if (!ContainsMission(Objective.MilksCount)) throw exception; }
    }

    public void InstantiateNewSheep()
    {
        var price = GetSetting(SettingsKey.SheepPrice);

        if (CurrentCoinsCount < price) return;
        CurrentCoinsCount -= price;
        InstantiateObject(sheepPrefab.gameObject, sheepsParent);

        try { AddProgress(Objective.SheepsCount, 1); }
        catch (Exception exception) { if (!ContainsMission(Objective.MeatsCount)) throw exception; }
    }

    public void FillWell()
    {
        var fillPrice = GetSetting(SettingsKey.WellFillPrice);
        if (CurrentCoinsCount >= fillPrice && Well.Instance.Fill())
            CurrentCoinsCount -= fillPrice;
    }

    #endregion
}
