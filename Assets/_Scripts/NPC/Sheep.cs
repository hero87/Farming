using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : Animal
{
    public override int EatingTime => LevelManager.Instance.GetSetting(SettingsKey.SheepEatingTime);

    public override int HungerTime => LevelManager.Instance.GetSetting(SettingsKey.SheepHungerTime);

    public override int MaxHealth => LevelManager.Instance.GetSetting(SettingsKey.SheepMaxHealth);

    public override int ChasingSpeed => LevelManager.Instance.GetSetting(SettingsKey.SheepChasingSpeed);

    public override int PatrollingRange => LevelManager.Instance.GetSetting(SettingsKey.SheepPatrollingRange);

    public override int PatrollingSpeed => LevelManager.Instance.GetSetting(SettingsKey.SheepPatrollingSpeed);
}
