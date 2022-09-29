using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : Animal
{
    public override int EatingTime => LevelManager.Instance.GetSetting(SettingsKey.ChickenEatingTime);

    public override int HungerTime => LevelManager.Instance.GetSetting(SettingsKey.ChickenHungerTime);

    public override int MaxHealth => LevelManager.Instance.GetSetting(SettingsKey.ChickenMaxHealth);

    public override int ChasingSpeed => LevelManager.Instance.GetSetting(SettingsKey.ChickenChasingSpeed);

    public override int PatrollingRange => LevelManager.Instance.GetSetting(SettingsKey.ChickenPatrollingRange);

    public override int PatrollingSpeed => LevelManager.Instance.GetSetting(SettingsKey.ChickenPatrollingSpeed);
}
