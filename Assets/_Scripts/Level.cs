using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Level 01", menuName = "Create New Level")]
public class Level : ScriptableObject
{
    // TODO add items & animals price
    [Header("Initial Conditions")]
    [SerializeField] private int coinsCount;
    [SerializeField] private int wellCapacity;

    [SerializeField] private bool allowChicknsCreating;
    [SerializeField] private bool allowCowsCreating;
    [SerializeField] private bool allowSheepsCreating;

    [SerializeField] private bool activateMilkFactory;
    [SerializeField] private bool activateMeatFactory;
    [SerializeField] private bool activateBurgerResturant;
    [SerializeField] private bool activateBreadBakery;
    [SerializeField] private bool activateCakeBeakery;

    [Header("Wining Conditions")]
    [SerializeField] private float goldTime;
    [SerializeField] private float maximumTime;

    // collectible items
    [SerializeField] private int eggsCount;
    [SerializeField] private int milkCount;
    [SerializeField] private int meatCount;
    [SerializeField] private int breadCount;
    [SerializeField] private int cakeCount;

    // animals
    [SerializeField] private int chickensCount;
    [SerializeField] private int cowsCount;
    [SerializeField] private int sheepsCount;



    // Initial Conditions
    public int CoinsCount => coinsCount;
    public int WellCapacity => wellCapacity;
    public bool AllowChicknsCreating => allowChicknsCreating;
    public bool AllowCowsCreating => allowCowsCreating;
    public bool AllowSheepsCreating => allowSheepsCreating;
    public bool ActivateMilkFactory => activateMilkFactory;
    public bool ActivateMeatFactory => activateMeatFactory;
    public bool ActivateBurgerResturant => activateBurgerResturant;
    public bool ActivateBreadBakery => activateBreadBakery;
    public bool ActivateCakeBeakery => activateCakeBeakery;



    // Wining Conditions
    public float GoldTime => goldTime;
    public float MaximumTime => maximumTime;
    public int EggsCount => eggsCount;
    public int MilkCount => milkCount;
    public int MeatCount => meatCount;
    public int BreadCount => breadCount;
    public int CakeCount => cakeCount;
    public int ChickensCount => chickensCount;
    public int CowsCount => cowsCount;
    public int SheepsCount => sheepsCount;

}
