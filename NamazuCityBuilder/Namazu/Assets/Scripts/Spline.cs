using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class Spline
{
    List<Vector2> Points;

    List<Vector2> Directions;

    public Vector2 EvaluatePosition(float t)
    {
        float tt = Mathf.Clamp(t, 0, 0.9999f) * (Points.Count - 1);
        int i = Mathf.FloorToInt(tt);
        float ttt = tt - i;

        Vector2 p0 = Points[i];
        Vector2 p1 = Points[i] + Directions[i];
        Vector2 p2 = Points[i + 1] - Directions[i + 1];
        Vector2 p3 = Points[i + 1];

        Vector2 p = ((1 - ttt) * (1 - ttt) * (1 - ttt) * p0) + (3 * ttt * (1 - ttt) * (1 - ttt) * p1) + (3 * ttt * ttt * (1 - ttt) * p2) + (ttt * ttt * ttt * p3);
        return p;
    }

    public Vector2 EvaluateDirection(float t)
    {
        float tt = Mathf.Clamp(t, 0, 0.9999f) * (Points.Count - 1);
        int i = Mathf.FloorToInt(tt);
        float ttt = tt - i;

        Vector2 p0 = Points[i];
        Vector2 p1 = Points[i] + Directions[i];
        Vector2 p2 = Points[i + 1] - Directions[i + 1];
        Vector2 p3 = Points[i + 1];

        Vector2 p = (3 * (1 - ttt) * (1 - ttt) * (p1 - p0)) + (6 * ttt * (1 - ttt) * (p2 - p1)) + (3 * ttt * ttt * (p3 - p2));
        return p;
    }

    public float EvaluateAngle(float t)
    {
        Vector2 dir = EvaluateDirection(t);
        return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }


    public void SetPoints(List<Vector2> points, List<Vector2> directions)
    {
        Points = new List<Vector2>(points);
        Directions = new List<Vector2>(directions);
    }
}
