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


    private HashSet<Mission.Key> completedMissions = new HashSet<Mission.Key>();


    public List<Mission> Missions => missions;
    public bool Completed => missions.Count == completedMissions.Count;


    public int GetSetting(Settings.Key key) => settings.GetValue(key);

    public void AddProgressTo(Mission.Key key)
    {
        var mission = missions.FirstOrDefault(m => m.Objective == key);
        if (mission == null) throw new Exception($"ERROR | key {key} not found in the missions list");

        mission.AddProgress();

        if (mission.Completed && !completedMissions.Contains(mission.Objective))
            completedMissions.Add(mission.Objective);
    }

    public int GetCurrentValueOf(Mission.Key key)
    {
        var mission = missions.FirstOrDefault(m => m.Objective == key);
        if (mission == null) throw new Exception($"ERROR | key {key} not found in the missions list");
        return mission.CurrentValue;
    }

    public bool Contains(Mission.Key key)
    {
        return missions.Any(m => m.Objective == key);
    }

    public void Initiate()
    {
        completedMissions.Clear();
        missions.ForEach(m => m.Initiate());
    }
}
