using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public int GetValueOf(TrackableType key) => storage.Find(kv => kv.key == key).value;

    public void AddToStorage(TrackableType key)
    {
        var item = storage.Find(temp => temp.key == key);

        if (item != null)
        {
            item.value++;
        }
        else
        {
            item = new Item(key, 0);
            storage.Add(item);
        }

        if (onValueChanged != null)
            onValueChanged(key, item.value);
    }

    public void MoveToTruck(TrackableType key)
    {
        var item = storage.Find(temp => temp.key == key);
        if (item.value <= 0) return;

        item.value--;
        Truck.Instance.AddToTruck(item.key);
        onValueChanged(key, item.value);
    }
}
