using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public static class Extensions
{
    public static Sprite GetSprite(TrackableType trackableType)
    {
        return Resources.Load<Sprite>($"Sprites/{System.Enum.GetName(typeof(TrackableType), trackableType)}");
    }

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

    public static bool IsCollectable(TrackableType trackableType)
    {
        return !(
            trackableType == TrackableType.ChickensCount ||
            trackableType == TrackableType.CowsCount ||
            trackableType == TrackableType.SheepsCount ||
            trackableType == TrackableType.CoinsCount
                 );
    }

    public static Settings.Key GetTrackableSettingsKey(TrackableType trackableType)
    {
        if (trackableType == TrackableType.EggsCount) return Settings.Key.EggPrice;
        else if (trackableType == TrackableType.MilksCount) return Settings.Key.MilkPrice;
        else if (trackableType == TrackableType.MeatsCount) return Settings.Key.MeatPrice;

        throw new System.Exception($"Cannot convert trackable item {trackableType}");
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