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
        new KeyValue(Key.GoldTime, 60),
        new KeyValue(Key.MaximumTime, 120),
        new KeyValue(Key.CollectableLifeTime, 20),
        new KeyValue(Key.ChicknPrice, 100),
        new KeyValue(Key.CowPrice, 200),
        new KeyValue(Key.SheepPrice, 250),
    };

    public int GetValue(Key _key)
    {
        var keyBalue = keyValueList.FirstOrDefault(m => m.key == _key);
        if (keyBalue == null) { throw new Exception($"ERROR | key {keyBalue} not found in the settings list"); }
        return keyBalue.value;

    }

}
