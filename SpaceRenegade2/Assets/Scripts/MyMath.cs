using UnityEngine;

public static class MyMath
{
    public static Vector2 AngleToVector(float angle)
    {
        return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
    }

    public static void AngleToVector(ref Vector2 outputVector, float angle)
    {
        outputVector.Set(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
    }
}
