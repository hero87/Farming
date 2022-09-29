using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(fileName = "Settings 01", menuName = "Create New Settings")]
public partial class Settings : ScriptableObject
{
    [Serializable]
    private class KeyValue
    {
        public SettingsKey key;
        public int value;

        public KeyValue(SettingsKey key, int value)
        {
            this.key = key;
            this.value = value;
        }
    }

    [SerializeField]
    private List<KeyValue> keyValueList = new List<KeyValue>
    {
        new KeyValue(SettingsKey.CoinsCount, 500),
        new KeyValue(SettingsKey.WellCapacity, 5),
        new KeyValue(SettingsKey.WellFillPrice, 100),

        new KeyValue(SettingsKey.GoldTime, 60_000),
        new KeyValue(SettingsKey.MaximumTime, 120_000),

        new KeyValue(SettingsKey.CollectableLifeTime, 20),

        new KeyValue(SettingsKey.ChicknPrice, 100),
        new KeyValue(SettingsKey.CowPrice, 200),
        new KeyValue(SettingsKey.SheepPrice, 300),

        new KeyValue(SettingsKey.EggPrice, 25),
        new KeyValue(SettingsKey.MilkPrice, 50),
        new KeyValue(SettingsKey.MeatPrice, 75),

        new KeyValue(SettingsKey.EggSize, 25),
        new KeyValue(SettingsKey.MilkSize, 25),
        new KeyValue(SettingsKey.MeatSize, 100),
        new KeyValue(SettingsKey.BreadSize, 50),
        new KeyValue(SettingsKey.CakeSize, 75),
        new KeyValue(SettingsKey.BurgerSize, 50),

        new KeyValue(SettingsKey.StorageCapacity, 250),
        new KeyValue(SettingsKey.TradeTime, 18_000),
        new KeyValue(SettingsKey.EnemyTime, 20_000),
        new KeyValue(SettingsKey.MaxEnemyNumber, 2),
        new KeyValue(SettingsKey.GrassRadius, 5),
        new KeyValue(SettingsKey.GrassRefreshRate, 250),
        new KeyValue(SettingsKey.PlayerHitDamage, 2_000),
        new KeyValue(SettingsKey.ProductsLifeTime, 10_000),
        new KeyValue(SettingsKey.EnemyKillAward, 100),

        new KeyValue(SettingsKey.ChickenMaxHealth, 10_000),
        new KeyValue(SettingsKey.CowMaxHealth, 20_000),
        new KeyValue(SettingsKey.SheepMaxHealth, 30_000),
        new KeyValue(SettingsKey.EnemyMaxHealth, 10_000),

        new KeyValue(SettingsKey.ChickenPatrollingRange, 5),
        new KeyValue(SettingsKey.CowPatrollingRange, 7),
        new KeyValue(SettingsKey.SheepPatrollingRange, 7),
        new KeyValue(SettingsKey.EnemyPatrollingRange, 15),

        new KeyValue(SettingsKey.ChickenPatrollingSpeed, 2),
        new KeyValue(SettingsKey.CowPatrollingSpeed, 2),
        new KeyValue(SettingsKey.SheepPatrollingSpeed, 2),
        new KeyValue(SettingsKey.EnemyPatrollingSpeed, 5),

        new KeyValue(SettingsKey.ChickenChasingSpeed, 5),
        new KeyValue(SettingsKey.SheepChasingSpeed, 5),
        new KeyValue(SettingsKey.CowChasingSpeed, 5),

        new KeyValue(SettingsKey.ChickenEatingTime, 4_000),
        new KeyValue(SettingsKey.SheepEatingTime, 4_000),
        new KeyValue(SettingsKey.CowEatingTime, 4_000),

        new KeyValue(SettingsKey.ChickenHungerTime, 5_000),
        new KeyValue(SettingsKey.SheepHungerTime, 10_000),
        new KeyValue(SettingsKey.CowHungerTime, 15_000),
    };

    private Dictionary<SettingsKey, int> settingsDict;
    public void Initiate()
    {
        settingsDict = new Dictionary<SettingsKey, int>();
        keyValueList.ForEach(kv => settingsDict.Add(kv.key, kv.value));
    }

    public int GetValue(SettingsKey key)
    {
        if (settingsDict.TryGetValue(key, out int value))
            return value;

        throw new Exception($"ERROR | key {key} not found in the settings list");
    }

}
