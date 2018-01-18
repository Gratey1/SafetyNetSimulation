using UnityEngine;
using System;
using System.Collections;

public enum CurveType
{
    None = 0,
    Linear = 1,
    EaseIn = 2,
    EaseOut = 3,
    EaseInEaseOut = 4,
}

public class CurveManager : OurMonoBehaviour
{
    private static CurveManager instance;
    public static CurveManager Instance { get { return instance; } }

    [System.Serializable]
    public class Curve
    {
        public CurveType TheType = CurveType.None;
        public AnimationCurve TheCurve;
    }

    [SerializeField]
    private Curve[] curves;

    private void Awake()
    {
        if (CurveManager.instance != null)
        {
            Debug.LogError("Multiple instances of CurveManager singleton!");
            Destroy(this);
            return;
        }

        CurveManager.instance = this;
    }

    public float EvaluateCurve(float start, float end, float t, CurveType curve)
    {
        return start + ((end - start) * EvaluateCurve(t, curve));
    }

    public Vector3 EvaluateCurve(Vector3 start, Vector3 end, float t, CurveType curve)
    {
        return start + ((end - start) * EvaluateCurve(t, curve));
    }

    public Color EvaluateCurve(Color start, Color end, float t, CurveType curve)
    {
        return start + ((end - start) * EvaluateCurve(t, curve));
    }

    public float EvaluateCurve(float t, CurveType curve)
    {
        if (curves != null)
        {
            Curve c = GetCurve(curve);
            if (c != null && c.TheCurve != null)
            {
                return c.TheCurve.Evaluate(t);
            }
        }
        return 0f;
    }

    public Curve GetCurve(CurveType curve)
    {
        if (curves != null)
        {
            for (int i = 0; i < curves.Length; i++)
            {
                var c = curves[i];
                if (c != null && c.TheCurve != null && c.TheType == curve)
                {
                    return c;
                }
            }
        }

        return null;
    }
}

