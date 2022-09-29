using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public static class Extensions
{
    public static Sprite GetSprite(Objective trackableType)
    {
        return Resources.Load<Sprite>($"Sprites/{System.Enum.GetName(typeof(Objective), trackableType)}");
    }


    // TODO Gives Error Sometimes
    public static bool GetRandomPoint(Vector3 center, float range, out Vector3 result)
    {
        result = Vector3.zero;
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        if (NavMesh.SamplePosition(randomPoint, out var hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }
        return false;
    }

    public static Quaternion GetRandomRotation() => Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up);

    public static bool IsCollectable(Objective trackableType)
    {
        return !(
            trackableType == Objective.ChickensCount ||
            trackableType == Objective.CowsCount ||
            trackableType == Objective.SheepsCount ||
            trackableType == Objective.CoinsCount
                 );
    }

    public static int GetTrackablePrice(Objective trackableType)
    {
        if (trackableType == Objective.EggsCount) return LevelManager.Instance.GetSetting(SettingsKey.EggPrice);
        else if (trackableType == Objective.MilksCount) return LevelManager.Instance.GetSetting(SettingsKey.MilkPrice);
        else if (trackableType == Objective.MeatsCount) return LevelManager.Instance.GetSetting(SettingsKey.MeatPrice);
        else throw new System.Exception($"Cannot convert trackable item {trackableType}");
    }

    public static int GetTrackableSize(Objective trackableType)
    {
        if (trackableType == Objective.EggsCount) return LevelManager.Instance.GetSetting(SettingsKey.EggSize);
        else if (trackableType == Objective.MilksCount) return LevelManager.Instance.GetSetting(SettingsKey.MilkSize);
        else if (trackableType == Objective.MeatsCount) return LevelManager.Instance.GetSetting(SettingsKey.MeatSize);
        else if (trackableType == Objective.BreadsCount) return LevelManager.Instance.GetSetting(SettingsKey.BreadSize);
        else if (trackableType == Objective.CakesCount) return LevelManager.Instance.GetSetting(SettingsKey.CakeSize);
        else if (trackableType == Objective.BurgersCount) return LevelManager.Instance.GetSetting(SettingsKey.BurgerSize);
        else throw new System.Exception($"Cannot find trackable item {trackableType} size");
    }

    public static bool IsInsideUI(Canvas canvas, Vector2 point)
    {
        foreach (var child in canvas.GetComponentsInChildren<RectTransform>())
        {
            if (child == canvas.GetComponent<RectTransform>()) continue;
            if (IsInsideUIRecursive(child, point) == true) return true;
        }
        return false;
    }

    private static bool IsInsideUIRecursive(RectTransform parent, Vector2 point)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(parent, point) == true) return true;
        foreach (var child in parent.GetComponentsInChildren<RectTransform>())
        {
            if (child == parent) continue;
            if (IsInsideUIRecursive(child, point) == true) return true;
        }
        return false;
    }
}