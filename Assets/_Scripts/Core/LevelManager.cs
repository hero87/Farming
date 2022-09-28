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
    [SerializeField] private GameObject burgerResturant;

    [Header("Instantiation Points")]
    [SerializeField] private float instantiationRange;
    [SerializeField] private Transform enemyParent;
    [SerializeField] private Transform chickensParent;
    [SerializeField] private Transform sheepsParent;
    [SerializeField] private Transform cowsParent;


    public int CurrentLevelNumber => PlayerPrefs.GetInt("CURRENT_LEVEL_NUMBER");
    public Level CurrentLevel => levels[CurrentLevelNumber];


    private int currentCoinsCount;
    public int CurrentCoinsCount
    {
        set
        {
            currentCoinsCount = value;
            UIManager.Instance.UpdateCoinText(value);
        }
        get => currentCoinsCount;
    }


    public Animal CowPrefab { get; private set; }
    public Animal SheepPrefab { get; private set; }
    public Animal ChickenPrefab { get; private set; }
    public Enemy EnemyPrefab { get; private set; }


    public static LevelManager Instance { get; private set; }

    private float time;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else throw new Exception("There is already a LevelManager object!");

        CowPrefab = Resources.Load<Animal>("NPCs/Cow");
        SheepPrefab = Resources.Load<Animal>("NPCs/Sheep");
        ChickenPrefab = Resources.Load<Animal>("NPCs/Chicken");
        EnemyPrefab = Resources.Load<Enemy>("NPCs/Dog");


        time =  CurrentLevel.GetSetting(Settings.Key.MaximumTime)/1000;
    }

    private void Update()
    {
        CheckLevelProgress();
        ManageEnemy();
    }

    private void Start()
    {
        CurrentLevel.Initiate();
        UIManager.Instance.Initiate();

        CurrentCoinsCount = CurrentLevel.GetSetting(Settings.Key.CoinsCount);
        Well.Instance.Capacity = CurrentLevel.GetSetting(Settings.Key.WellCapacity);

        meatFactory.SetActive(CurrentLevel.Contains(TrackableType.MeatsCount));
        cakeBakery.SetActive(CurrentLevel.Contains(TrackableType.CakesCount));
        breadBakery.SetActive(CurrentLevel.Contains(TrackableType.BreadsCount));
        burgerResturant.SetActive(CurrentLevel.Contains(TrackableType.HamburgersCount));
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


 

    private void CheckLevelProgress()
    {


       this.time -=  Time.deltaTime;
        
        var timeString = $"{(int)time}";

        if (CurrentLevel.Completed)
        {
            if (time <= CurrentLevel.GetSetting(Settings.Key.GoldTime) / 1000.0f) UIManager.Instance.ViewWinPanel("وقت ذهبي!", timeString);
            else UIManager.Instance.ViewWinPanel("أحسنت!", timeString);
            enabled = false;
        }

        //if (time >= 0)
        //{
        //    UIManager.Instance.ViewLosePanel();
        //    enabled = false;
        //}

        UIManager.Instance.SetTime(timeString);
    }

    public int NumberOfActiveEnemies { get; set; }
    private float enemyLastTime;
    private void ManageEnemy()
    {
        var shouldAddNewEnemy = Time.time - enemyLastTime > CurrentLevel.GetSetting(Settings.Key.EnemyTime) / 1000.0f &&
            NumberOfActiveEnemies < CurrentLevel.GetSetting(Settings.Key.MaxEnemyNumber);

        if (shouldAddNewEnemy)
        {
            InstantiateObject(EnemyPrefab.gameObject, enemyParent);
            enemyLastTime = Time.time;
        }
    }

    public int GetSetting(Settings.Key key) => CurrentLevel.GetSetting(key);
    public void AddProgress(TrackableType key, int value) => CurrentLevel.AddProgress(key, value);


    #region Events

    public void InstantiateNewChicken()
    {
        var price = CurrentLevel.GetSetting(Settings.Key.ChicknPrice);

        if (CurrentCoinsCount < price) return;
        CurrentCoinsCount -= price;
        InstantiateObject(ChickenPrefab.gameObject, chickensParent);

        try { CurrentLevel.AddProgress(TrackableType.ChickensCount, 1); }
        catch (Exception exception) { if (!CurrentLevel.Contains(TrackableType.EggsCount)) throw exception; }
    }

    public void InstantiateNewCow()
    {
        var price = CurrentLevel.GetSetting(Settings.Key.CowPrice);

        if (CurrentCoinsCount < price) return;
        CurrentCoinsCount -= price;
        InstantiateObject(CowPrefab.gameObject, cowsParent);

        try { CurrentLevel.AddProgress(TrackableType.CowsCount, 1); }
        catch (Exception exception) { if (!CurrentLevel.Contains(TrackableType.MilksCount)) throw exception; }
    }

    public void InstantiateNewSheep()
    {
        var price = CurrentLevel.GetSetting(Settings.Key.SheepPrice);

        if (CurrentCoinsCount < price) return;
        CurrentCoinsCount -= price;
        InstantiateObject(SheepPrefab.gameObject, sheepsParent);

        try { CurrentLevel.AddProgress(TrackableType.SheepsCount, 1); }
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
