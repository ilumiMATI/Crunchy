using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Redirect : MonoBehaviour
{
    [Range(0.0f, 360f)][SerializeField] private float redirectAngle = 45f;

    public float GetRandomRedirectAngleRad()
    {
        return GetRandomRedirectAngleDeg() * Mathf.Deg2Rad;
    }

    public float GetRandomRedirectAngleDeg()
    {
        int randomMultiplier = Random.Range(1, 5);
        return (redirectAngle + randomMultiplier * 90f);
    }

    public static Vector2 RotateVector2Up(Vector2 vector,float maxRandomAngle)
    {
        float randomAngle = Random.Range(-maxRandomAngle, maxRandomAngle) * Mathf.Deg2Rad;
        Vector2 directionVector = RotateVector2(Vector2.up, randomAngle);
        return directionVector * vector.magnitude;
    }

    public static Vector2 RotateVector2Down(Vector2 vector, float maxRandomAngle)
    {
        float randomAngle = Random.Range(-maxRandomAngle, maxRandomAngle) * Mathf.Deg2Rad;
        Vector2 directionVector = RotateVector2(Vector2.down, randomAngle);
        return directionVector * vector.magnitude;
    }

    public float GetRedirectAngleDeg()
    {
        return redirectAngle;
    }
    public float GetRedirectAngleRad()
    {
        return redirectAngle * Mathf.Deg2Rad;
    }

    public static Vector2 RotateVector2(Vector2 vector, float angleInRad)
    {
        Vector2 rotatedVector = new Vector2(
            vector.x * Mathf.Cos(angleInRad) - vector.y * Mathf.Sin(angleInRad),
            vector.x * Mathf.Sin(angleInRad) + vector.y * Mathf.Cos(angleInRad));
        return rotatedVector;
    }
}
