using UnityEngine;
using System.Collections.Generic;

static class Extensions
{
    public static Vector3 InXZPlane(this Vector3 vector, float y)
    {
        return new Vector3(vector.x, y, vector.z);
    }

    public static Vector2Int To2dInt(this Vector3 position)
    {
        return new Vector2Int(Mathf.FloorToInt(position.x), Mathf.FloorToInt(position.z));
    }

    public static T PickRandom<T>(this List<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }
    
    public static bool IsNear(this Vector3 position, Vector3 target, float distanceThreshold = 1.0f) {
        return Vector3.Distance(position, target) < distanceThreshold;
    }
}