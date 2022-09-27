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

        TradeTime,
        EnemyTime,
        MaxEnemyNumber,
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
        new KeyValue(Key.CoinsCount, 300),
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

        new KeyValue(Key.TradeTime, 18_000),
        new KeyValue(Key.EnemyTime, 20_000),
        new KeyValue(Key.MaxEnemyNumber, 2),
    };

    public int GetValue(Key key)
    {
        var keyValue = keyValueList.FirstOrDefault(m => m.key == key);
        if (keyValue == null) throw new Exception($"ERROR | key {keyValue} not found in the settings list");
        return keyValue.value;
    }

}
