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

    private HashSet<Objective> completedMissions;
    private HashSet<Objective> activeMissions;


    public bool IsCompleted => missions.Count == completedMissions.Count;
    public Objective[] Objectives => activeMissions.ToArray();


    public int GetSetting(SettingsKey key) => settings.GetValue(key);
    public Mission GetMission(Objective key) => missions.Find(mission => mission.Key == key);
    public bool Contains(Objective key) => missions.Any(m => m.Key == key);

    public void AddProgress(Objective key, int value)
    {
        var mission = missions.First(m => m.Key == key);
        if (mission == null) throw new Exception($"ERROR | key {key} not found in the missions list");

        mission.AddProgress(value);

        if (mission.IsCompleted && !completedMissions.Contains(mission.Key))
            completedMissions.Add(mission.Key);
    }

    public void Initiate()
    {
        settings.Initiate();
        completedMissions = new HashSet<Objective>();
        activeMissions = new HashSet<Objective>();

        missions.ForEach(mission =>
        {
            try { activeMissions.Add(mission.Key); }
            catch { throw new Exception($"Mission {mission.Key} already exists in the mission list of the level {name}"); }
            mission.Initiate();
        });
    }
}
