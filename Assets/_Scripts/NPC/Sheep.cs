using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : Animal
{
    public override int EatingTime => LevelManager.Instance.GetSetting(Settings.Key.SheepEatingTime);

    public override int HungerTime => LevelManager.Instance.GetSetting(Settings.Key.SheepHungerTime);

    public override int MaxHealth => LevelManager.Instance.GetSetting(Settings.Key.SheepMaxHealth);

    public override int ChasingSpeed => LevelManager.Instance.GetSetting(Settings.Key.SheepChasingSpeed);

    public override int PatrollingRange => LevelManager.Instance.GetSetting(Settings.Key.SheepPatrollingRange);

    public override int PatrollingSpeed => LevelManager.Instance.GetSetting(Settings.Key.SheepPatrollingSpeed);
}
