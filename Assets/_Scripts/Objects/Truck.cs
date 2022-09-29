using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class Truck : MonoBehaviour
{
    [SerializeField] private Animator truckAnimator;


    public static Truck Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else throw new Exception("There is already a Truck object!");
    }

    private List<Item> truck = new List<Item>();
    public Action<Objective, int> onValueChanged;
    public bool IsTradingActive { get; private set; }

    public int TotalPrice => truck.Sum(item => item.amount * Extensions.GetTrackablePrice(item.key));

    public void AddToTruck(Objective key, int amount)
    {
        var item = truck.Find(temp => temp.key == key);

        if (item != null) item.amount += amount;
        else truck.Add(new Item(key, amount));

        onValueChanged?.Invoke(key, item != null ? item.amount : amount);
    }

    public void MoveToStorage(Objective key, int amount)
    {
        var item = truck.Find(temp => temp.key == key);
        if (item.amount - amount < 0) return;

        Storage.Instance.AddToStorage(item.key, amount);
        item.amount -= amount;
        onValueChanged(item.key, item.amount);
    }

    public void MoveAllToStorage() => truck.ToList().ForEach(item =>
    {
        if (item.amount <= 0) return;

        Storage.Instance.AddToStorage(item.key, item.amount);
        item.amount = 0;
        onValueChanged(item.key, item.amount);
    });

    public async void ConfirmTrade()
    {
        IsTradingActive = true;
        truckAnimator.Play("Go");
        await Task.Delay(LevelManager.Instance.GetSetting(SettingsKey.TradeTime) * 2 / 3);

        truckAnimator.Play("Back");
        await Task.Delay(LevelManager.Instance.GetSetting(SettingsKey.TradeTime) * 1 / 3);
        IsTradingActive = false;

        LevelManager.Instance.CurrentCoinsCount += TotalPrice;
        try { LevelManager.Instance.AddProgress(Objective.CoinsCount, TotalPrice); }
        catch { }

        truck.ForEach(item =>
        {
            item.amount = 0;
            onValueChanged(item.key, item.amount);
        });
    }
}
