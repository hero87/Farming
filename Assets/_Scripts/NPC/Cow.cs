using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : Animal
{
    public override int EatingTime => LevelManager.Instance.GetSetting(Settings.Key.CowEatingTime);

    public override int HungerTime => LevelManager.Instance.GetSetting(Settings.Key.CowHungerTime);

    public override int MaxHealth => LevelManager.Instance.GetSetting(Settings.Key.CowMaxHealth);

    public override int ChasingSpeed => LevelManager.Instance.GetSetting(Settings.Key.CowChasingSpeed);

    public override int PatrollingRange => LevelManager.Instance.GetSetting(Settings.Key.CowPatrollingRange);

    public override int PatrollingSpeed => LevelManager.Instance.GetSetting(Settings.Key.CowPatrollingSpeed);
}
