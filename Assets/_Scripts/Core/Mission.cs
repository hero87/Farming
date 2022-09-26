using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTLTMPro;
using UnityEngine.UI;
using System.Reflection;


[CreateAssetMenu(fileName = "Mission 01", menuName = "Create New Mission")]
public class Mission : ScriptableObject
{
    [SerializeField] private TrackableType key;
    [SerializeField] private int targetValue;

    public TrackableType Key => key;
    public int TargetValue => targetValue;
    public bool Completed => CurrentValue >= targetValue;
    public int CurrentValue { get; private set; }

    public Action onAddProgress;

    public void Initiate()
    {
        CurrentValue = 0;
        if (Key == TrackableType.ChickensCount || Key == TrackableType.CowsCount || Key == TrackableType.SheepsCount || key == TrackableType.CoinsCount)
            return;
        Storage.Instance.AddToStorage(key);
        Truck.Instance.AddToStorage(key);
    }

    public void AddProgress()
    {
        CurrentValue++;
        onAddProgress();
        Storage.Instance.IncreaseValueOf(key);
    }
}


