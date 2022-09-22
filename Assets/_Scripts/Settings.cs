using System;
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

    [SerializeField] private KeyValue[] keyValueList = new KeyValue[]
    {
        new KeyValue(Key.CoinsCount, 300),
        new KeyValue(Key.WellCapacity, 5),
        new KeyValue(Key.WellFillPrice, 100),
        new KeyValue(Key.GoldTime, 60),
        new KeyValue(Key.MaximumTime, 120),

        new KeyValue(Key.CollectableLifeTime, 20),

        new KeyValue(Key.ChicknPrice, 100),
        new KeyValue(Key.CowPrice, 200),
        new KeyValue(Key.SheepPrice, 250),
    };

    public int GetValue(Key key)
    {
        foreach (var kv in keyValueList)
        {
            if (kv.key == key)
                return kv.value;
        }

        throw new Exception($"ERROR | key {key} not found in the settings list");
    }

}
