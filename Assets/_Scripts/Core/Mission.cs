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


    [SerializeField] private Key objective;
    [SerializeField] private int targetValue;


    public Key Objective => objective;
    public int TargetValue => targetValue;
    public bool Completed => CurrentValue >= targetValue;
    public int CurrentValue { get; private set; }

    public Action OnAddProgress;

    public void Initiate() => CurrentValue = 0;
    public void AddProgress() { CurrentValue++; OnAddProgress(); }
}


