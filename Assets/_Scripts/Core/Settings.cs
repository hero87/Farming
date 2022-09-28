using System;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(fileName = "Settings 01", menuName = "Create New Settings")]
public class Settings : ScriptableObject
{
    public enum Key
    {
        CoinsCount,
        WellCapacity,
        WellFillPrice,

        GoldTime,
        MaximumTime,

        CollectableLifeTime,

        ChicknPrice,
        CowPrice,
        SheepPrice,

        EggPrice,
        MilkPrice,
        MeatPrice,

        EggSize,
        MilkSize,
        MeatSize,
        BreadSize,
        CakeSize,
        HamburgerSize,

        StorageCapacity,
        TradeTime,
        EnemyTime,
        MaxEnemyNumber,
        GrassRadius,
        GrassRefreshRate,
        PlayerHitDamage,
        ProductsLifeTime,

        ChickenMaxHealth,
        SheepMaxHealth,
        CowMaxHealth,
        EnemyMaxHealth,

        ChickenPatrollingRange,
        SheepPatrollingRange,
        CowPatrollingRange,
        EnemyPatrollingRange,

        ChickenPatrollingSpeed,
        SheepPatrollingSpeed,
        CowPatrollingSpeed,
        EnemyPatrollingSpeed,

        ChickenChasingSpeed,
        SheepChasingSpeed,
        CowChasingSpeed,

        ChickenEatingTime,
        SheepEatingTime,
        CowEatingTime,
        
        ChickenHungerTime,
        SheepHungerTime,
        CowHungerTime,
    }


    [Serializable]
    public class KeyValue
    {
        public Key key;
        public int value;

        public KeyValue(Key key, int value)
        {
            this.key = key;
            this.value = value;
        }
    }

    [SerializeField]
    private KeyValue[] keyValueList = new KeyValue[]
    {
        new KeyValue(Key.CoinsCount, 500),
        new KeyValue(Key.WellCapacity, 5),
        new KeyValue(Key.WellFillPrice, 100),

        new KeyValue(Key.GoldTime, 60_000),
        new KeyValue(Key.MaximumTime, 120_000),

        new KeyValue(Key.CollectableLifeTime, 20),

        new KeyValue(Key.ChicknPrice, 100),
        new KeyValue(Key.CowPrice, 200),
        new KeyValue(Key.SheepPrice, 300),

        new KeyValue(Key.EggPrice, 25),
        new KeyValue(Key.MilkPrice, 50),
        new KeyValue(Key.MeatPrice, 75),

        new KeyValue(Key.EggSize, 25),
        new KeyValue(Key.MilkSize, 25),
        new KeyValue(Key.MeatSize, 100),
        new KeyValue(Key.BreadSize, 50),
        new KeyValue(Key.CakeSize, 75),
        new KeyValue(Key.HamburgerSize, 50),

        new KeyValue(Key.StorageCapacity, 250),
        new KeyValue(Key.TradeTime, 18_000),
        new KeyValue(Key.EnemyTime, 20_000),
        new KeyValue(Key.MaxEnemyNumber, 2),
        new KeyValue(Key.GrassRadius, 5),
        new KeyValue(Key.GrassRefreshRate, 250),
        new KeyValue(Key.PlayerHitDamage, 2_000),
        new KeyValue(Key.ProductsLifeTime, 10_000),

         new KeyValue(Key.ChickenMaxHealth, 10_000),
         new KeyValue(Key.CowMaxHealth, 20_000),
         new KeyValue(Key.SheepMaxHealth, 30_000),
         new KeyValue(Key.EnemyMaxHealth, 10_000),

         new KeyValue(Key.ChickenPatrollingRange, 5),
         new KeyValue(Key.CowPatrollingRange, 7),
         new KeyValue(Key.SheepPatrollingRange, 7),
         new KeyValue(Key.EnemyPatrollingRange, 15),

         new KeyValue(Key.ChickenPatrollingSpeed, 2),
         new KeyValue(Key.CowPatrollingSpeed, 2),
         new KeyValue(Key.SheepPatrollingSpeed, 2),
         new KeyValue(Key.EnemyPatrollingSpeed, 5),

         new KeyValue(Key.ChickenChasingSpeed, 5),
         new KeyValue(Key.SheepChasingSpeed, 5),
         new KeyValue(Key.CowChasingSpeed, 5),

         new KeyValue(Key.ChickenEatingTime, 4_000),
         new KeyValue(Key.SheepEatingTime, 4_000),
         new KeyValue(Key.CowEatingTime, 4_000),

         new KeyValue(Key.ChickenHungerTime, 5_000),
         new KeyValue(Key.SheepHungerTime, 10_000),
         new KeyValue(Key.CowHungerTime, 15_000),
    };

    public int GetValue(Key key)
    {
        var keyValue = keyValueList.FirstOrDefault(m => m.key == key);
        if (keyValue == null) throw new Exception($"ERROR | key {key} not found in the settings list");
        return keyValue.value;
    }

}
