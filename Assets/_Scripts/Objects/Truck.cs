using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck : MonoBehaviour
{
    public static Truck Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else throw new Exception("There is already a Truck object!");
    }

    public Action<TrackableType, int> onValueChanged;
    private List<Item> truck = new List<Item>();

    public void AddToStorage(TrackableType key) => truck.Add(new Item(key, 0));
    public int GetValueOf(TrackableType key) => truck.Find(kv => kv.key == key).value;

    public void IncreaseValueOf(TrackableType key)
    {
        var vr = truck.Find(kv => kv.key == key);
        vr.value++;
        onValueChanged(key, vr.value);
    }

    public void DecreaseValueOf(TrackableType key)
    {
        var vr = truck.Find(kv => kv.key == key);
        vr.value--;
        onValueChanged(key, vr.value);
    }
}
