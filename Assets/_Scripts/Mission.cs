using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(fileName = "Mission 01", menuName = "Create New Mission")]
public class Mission : ScriptableObject
{
    public enum Key
    {
        EggsCount,
        MilkCount,
        MeatCount,
        BurgerCount,
        BreadCount,
        CakeCount,
        ChickensCount,
        CowsCount,
        SheepsCount,
    }

    [SerializeField] private Key key;
    [SerializeField] private int targetValue;

    public Key GetKey => key;
    public int GetTargetValue => targetValue;
    public int GetCurrentValue => currentValue;
    public bool Completed => currentValue >= targetValue;


    private int currentValue;
    public void AddProgress() => currentValue++;

    public void Reset() => currentValue = 0;
}
