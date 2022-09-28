using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : Animal
{
    public override int EatingTime => LevelManager.Instance.GetSetting(Settings.Key.ChickenEatingTime);

    public override int HungerTime => LevelManager.Instance.GetSetting(Settings.Key.ChickenHungerTime);

    public override int MaxHealth => LevelManager.Instance.GetSetting(Settings.Key.ChickenMaxHealth);

    public override int ChasingSpeed => LevelManager.Instance.GetSetting(Settings.Key.ChickenChasingSpeed);

    public override int PatrollingRange => LevelManager.Instance.GetSetting(Settings.Key.ChickenPatrollingRange);

    public override int PatrollingSpeed => LevelManager.Instance.GetSetting(Settings.Key.ChickenPatrollingSpeed);
}
