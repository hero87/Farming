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


    private HashSet<TrackableType> completedMissions = new HashSet<TrackableType>();


    public List<Mission> Missions => missions;
    public bool Completed => missions.Count == completedMissions.Count;


    public int GetSetting(Settings.Key key) => settings.GetValue(key);

    public void AddProgress(TrackableType key, int value)
    {
        var mission = missions.FirstOrDefault(m => m.Key == key);
        if (mission == null) throw new Exception($"ERROR | key {key} not found in the missions list");

        mission.AddProgress(value);

        if (mission.Completed && !completedMissions.Contains(mission.Key))
            completedMissions.Add(mission.Key);
    }

    public int GetCurrentValueOf(TrackableType key)
    {
        var mission = missions.FirstOrDefault(m => m.Key == key);
        if (mission == null) throw new Exception($"ERROR | key {key} not found in the missions list");
        return mission.CurrentValue;
    }

    public bool Contains(TrackableType key)
    {
        return missions.Any(m => m.Key == key);
    }

    public void Initiate()
    {
        completedMissions.Clear();
        missions.ForEach(m => m.Initiate());
    }
}
