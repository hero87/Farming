using UnityEngine;
using UnityEngine.AI;

public static class Extensions
{
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
}