using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
    public int TotalPrice => truck.Sum(item => item.value * Extensions.GetTrackablePrice(item.key));

    public void AddToTruck(TrackableType key)
    {
        var item = truck.Find(temp => temp.key == key);

        if (item != null)
        {
            item.value++;
        }
        else
        {
            item = new Item(key, 0);
            truck.Add(item);
        }

        onValueChanged?.Invoke(key, item.value);
    }

    public void MoveToStorage(TrackableType key)
    {
        var item = truck.Find(temp => temp.key == key);
        if (item.value <= 0) return;

        item.value--;
        Storage.Instance.AddToStorage(item.key);
        onValueChanged(key, item.value);
    }

    public void MoveAllToStorage() => truck.ForEach(item => MoveToStorage(item.key));

    public void ConfirmTrade()
    {
        LevelManager.Instance.CurrentCoinsCount += TotalPrice;

        try { LevelManager.Instance.AddProgress(TrackableType.CoinsCount, TotalPrice); }
        catch (Exception) { }

        foreach (var item in truck)
        {
            item.value = 0;
            onValueChanged(item.key, item.value);
        }
    }
}
