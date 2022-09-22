using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using System;
using System.Reflection;

[CreateAssetMenu(fileName = "Level 01", menuName = "Create New Level")]
public class Level : ScriptableObject
{
    [SerializeField] private Settings settings;
    [SerializeField] private List<Mission> missions;

    public int GetSetting(Settings.Key key) => settings.GetValue(key);

    private int completedMissionsCount;
  
    public void AddProgressTo(Mission.Key key)
    {
        var mission = missions.FirstOrDefault(m => m.GetKey == key);
        if (mission == null) throw new Exception($"ERROR | key {key} not found in the missions list");
        // TODO allow the player to save new animals & items into inventory
        if (mission.Completed) return;
        mission.AddProgress();
        if (mission.Completed) completedMissionsCount++;
    }

    public int GetCurrentValueOf(Mission.Key key)
    {
        var mission = missions.FirstOrDefault(m => m.GetKey == key);
        if (mission == null) throw new Exception($"ERROR | key {key} not found in the missions list");
        return mission.GetCurrentValue;
    }

    public bool Contains(Mission.Key key)
    {
        return missions.Any(m => m.GetKey == key);
    }

    public bool Completed => missions.Count == completedMissionsCount;

    public void Reset()
    {
        completedMissionsCount = 0;
        missions.ForEach(m => m.Reset());   
       
    }
}
