using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Level 01", menuName = "Create New Level")]
public class Level : ScriptableObject
{
    [SerializeField] private Settings settings;
    [SerializeField] private List<Mission> missions;

    public int GetSetting(Settings.Key key) => settings.GetValue(key);

    private int completedMissionsCount;
    public void AddProgressTo(Mission.Key key)
    {
        for (int i = 0; i < missions.Count; i++)
        {
            var mission = missions[i];
            if (mission.GetKey == key && !mission.Completed)
            {
                mission.AddProgress();
                if (mission.Completed) completedMissionsCount++;
                return;
            }
        }

        throw new Exception($"ERROR | key {key} not found in the missions list");
    }

    public int GetCurrentValueOf(Mission.Key key)
    {
        for (int i = 0; i < missions.Count; i++)
        {
            var mission = missions[i];
            if (mission.GetKey == key) return mission.GetCurrentValue;
        }

        throw new Exception($"ERROR | key {key} not found in the missions list");
    }

    public bool Contains(Mission.Key key)
    {
        for (int i = 0; i < missions.Count; i++)
        {
            var mission = missions[i];
            if (mission.GetKey == key) return true;
        }
        return false;
    }

    public bool Completed => missions.Count == completedMissionsCount;

    public void Reset()
    {
        completedMissionsCount = 0;
        foreach (var mission in missions) mission.Reset();
    }
}
