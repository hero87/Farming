using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public static Storage Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else throw new Exception("There is already a Storage object!");
    }

    public Action<TrackableType, int> onValueChanged;
    private List<Item> storage = new List<Item>();

    public void AddToStorage(TrackableType key) => storage.Add(new Item(key, 0));
    public int GetValueOf(TrackableType key) => storage.Find(kv => kv.key == key).value;

    public void IncreaseValueOf(TrackableType key)
    {
        var vr = storage.Find(kv => kv.key == key);
        vr.value++;
        onValueChanged(key, vr.value);
    }
    public void DecreaseValueOf(TrackableType key)
    {
        var vr = storage.Find(kv => kv.key == key);
        vr.value--;
        onValueChanged(key, vr.value);
    }
}
