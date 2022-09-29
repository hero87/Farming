using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : Animal
{
    public override int EatingTime => LevelManager.Instance.GetSetting(SettingsKey.CowEatingTime);

    public override int HungerTime => LevelManager.Instance.GetSetting(SettingsKey.CowHungerTime);

    public override int MaxHealth => LevelManager.Instance.GetSetting(SettingsKey.CowMaxHealth);

    public override int ChasingSpeed => LevelManager.Instance.GetSetting(SettingsKey.CowChasingSpeed);

    public override int PatrollingRange => LevelManager.Instance.GetSetting(SettingsKey.CowPatrollingRange);

    public override int PatrollingSpeed => LevelManager.Instance.GetSetting(SettingsKey.CowPatrollingSpeed);
}
