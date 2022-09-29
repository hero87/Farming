using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.Port;

public class Storage : MonoBehaviour
{
    public static Storage Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else throw new Exception("There is already a Storage object!");
    }

    private void Start() => Capacity = LevelManager.Instance.GetSetting(SettingsKey.StorageCapacity);

    public Action<Objective, int> onValueChanged;
    private List<Item> storage = new List<Item>();

    public int Capacity { get; private set; }


    private int occupiedSpace;
    public int OccupiedSpace
    {
        get => occupiedSpace;
        private set
        {
            occupiedSpace = value;
            var percentage = 1.0f * occupiedSpace / Capacity;
            UIManager.Instance.UpdateStorageSpace(percentage);
        }
    }


    public void AddToStorage(Objective key, int amount)
    {
        if (Extensions.GetTrackableSize(key) * amount + OccupiedSpace > Capacity) return;
        else OccupiedSpace += Extensions.GetTrackableSize(key) * amount;

        var item = storage.Find(temp => temp.key == key);

        if (item != null) item.amount += amount;
        else storage.Add(new Item(key, amount));

        onValueChanged?.Invoke(key, item != null ? item.amount : amount);
    }

    public void MoveToTruck(Objective key, int amount)
    {
        var item = storage.Find(temp => temp.key == key);
        if (item.amount - amount < 0) return;

        Truck.Instance.AddToTruck(item.key, amount);
        OccupiedSpace -= Extensions.GetTrackableSize(key) * amount;
        item.amount -= amount;
        onValueChanged(item.key, item.amount);
    }
}
