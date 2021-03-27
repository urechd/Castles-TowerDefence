using UnityEngine;

public static class Utils
{
    public static float AngleBetweenTwoVectors(Vector2 v1, Vector2 v2)
    {
        return Vector2.Angle(v1, v2);
    }
}