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
    public Action onAddProgress;

    [SerializeField] private Objective key;
    [SerializeField] private int targetValue;

    public Objective Key => key;
    public int TargetValue => targetValue;
    public bool IsCompleted => CurrentValue >= targetValue;
    public int CurrentValue { get; private set; }

    public void Initiate()
    {
        CurrentValue = 0;
        if (!Extensions.IsCollectable(Key)) return;

        Storage.Instance.AddToStorage(key, 0);
        Truck.Instance.AddToTruck(key, 0);
    }

    public void AddProgress(int value)
    {
        CurrentValue += value;
        try { Storage.Instance.AddToStorage(key, value); }
        catch { }
        onAddProgress();
    }
}


