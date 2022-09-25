using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTLTMPro;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Mission 01", menuName = "Create New Mission")]
public class Mission : ScriptableObject
{
    public enum Key
    {
        EggsCount,
        MilksCount,
        MeatsCount,
        BurgersCount,
        BreadsCount,
        CakesCount,
        ChickensCount,
        CowsCount,
        SheepsCount,
    }


    [SerializeField] private Key key;
    [SerializeField] private int targetValue;
    [SerializeField] private Sprite sprite;


    public Key GetKey => key;
    public int GetTargetValue => targetValue;
    public int GetCurrentValue => currentValue;
    public bool Completed => currentValue >= targetValue;

    private int currentValue;

    public void Initiate()
    {
        currentValue = 0;

    }
    public void AddProgress()
    {
        currentValue++;
        
    }

}


