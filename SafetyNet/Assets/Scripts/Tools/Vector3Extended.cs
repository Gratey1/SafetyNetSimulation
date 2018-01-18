using UnityEngine;

public static class Vector3Extended
{
    public static Vector3 Rotate(this Vector3 me, Vector3 axis, float angle)
    {
        var rot = Quaternion.AngleAxis(angle, axis);
        return rot * me;
    }

    public static Vector3 NearestPointOnLine(this Vector3 pnt, Vector3 linePnt, Vector3 lineDir)
    {
        lineDir.Normalize();//this needs to be a unit vector
        var v = pnt - linePnt;
        var d = Vector3.Dot(v, lineDir);
        return linePnt + lineDir * d;
    }

    public static Vector3 NearestPointOnLineSegment(this Vector3 point, Vector3 segmentStartPoint, Vector3 segmentEndPoint)
    {
        float t;
        return point.NearestPointOnLineSegment(segmentStartPoint, segmentEndPoint, out t);
    }

    public static Vector3 NearestPointOnLineSegment(this Vector3 point, Vector3 segmentStartPoint, Vector3 segmentEndPoint, out float t)
    {
        Vector3 startToEnd = segmentEndPoint - segmentStartPoint;
        Vector3 startToPoint = point - segmentStartPoint;

        float c1 = Vector3.Dot(startToPoint, startToEnd);
        if (c1 <= 0)
        {
            t = 0.0f;
            return segmentStartPoint;
        }

        float c2 = Vector3.Dot(startToEnd, startToEnd);
        if (c2 <= c1)
        {
            t = 0.0f;
            return segmentEndPoint;
        }

        t = c1 / c2;
        return segmentStartPoint + t * startToEnd;
    }

    public static Vector3 SmoothApproach(this Vector3 position, Vector3 prevTargetPosition, Vector3 targetPosition, float speed)
    {
        float t = Time.deltaTime * speed;
        Vector3 v = (targetPosition - prevTargetPosition) / t;
        Vector3 f = position - prevTargetPosition + v;
        return targetPosition - v + f * Mathf.Exp(-t);
    }
}
