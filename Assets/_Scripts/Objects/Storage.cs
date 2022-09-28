using System;
using System.Collections;
using System.Collections.Generic;
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

    private void Start() => Capacity = LevelManager.Instance.GetSetting(Settings.Key.StorageCapacity);

    public Action<TrackableType, int> onValueChanged;
    private List<Item> storage = new List<Item>();


    public int Capacity { get; private set; }


    private int occupiedSpace;
    public int OccupiedSpace { 
        get => occupiedSpace;
        private set
        {
            occupiedSpace = value;
            var percentage = 1.0f * occupiedSpace / Capacity;
            UIManager.Instance.UpdateStorageSpace(percentage);
        }
    }

    public int GetValueOf(TrackableType key) => storage.Find(kv => kv.key == key).value;

    public void AddToStorage(TrackableType key)
    {
        if (Extensions.GetTrackableSize(key) + OccupiedSpace > Capacity) return;

        var item = storage.Find(temp => temp.key == key);
        OccupiedSpace += Extensions.GetTrackableSize(key);

        if (item != null)
        {
            item.value++;
        }
        else
        {
            item = new Item(key, 1);
            storage.Add(item);
        }

        onValueChanged?.Invoke(key, item.value);
    }

    public void MoveToTruck(TrackableType key)
    {
        var item = storage.Find(temp => temp.key == key);
        if (item.value <= 0) return;

        item.value--;
        OccupiedSpace -= Extensions.GetTrackableSize(key);
        Truck.Instance.AddToTruck(item.key);
        onValueChanged(key, item.value);
    }
}
