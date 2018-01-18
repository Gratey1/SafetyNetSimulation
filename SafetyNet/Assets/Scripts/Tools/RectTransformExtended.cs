using UnityEngine;
using System.Collections;

public enum VerticalAlignment
{
    Bottom,
    Middle,
    Top,
}

public enum HorizontalAlignment
{
    Left, 
    Middle, 
    Right,
}

public static class RectTransformExtended
{
    public static void AlignOnScreen(this RectTransform _rt, HorizontalAlignment _hAlignment = HorizontalAlignment.Middle, VerticalAlignment _vAlignment = VerticalAlignment.Middle)
    {
        float x = GetHorizontalAlignment(_hAlignment, ref _rt);
        float y = GetVerticalAlignment(_vAlignment, ref _rt);
        _rt.anchoredPosition = new Vector2(x, y);
    }

    private static float GetHorizontalAlignment(HorizontalAlignment _hAlignment, ref RectTransform _rt)
    {
        Canvas _c = _rt.parent.GetComponent<Canvas>();
        if (_c == null)
        {
            Debug.LogError("No Canvas!");
            return 0;
        }

        RectTransform _cRt = _c.GetComponent<RectTransform>();

        float xPos = 0;
        switch(_hAlignment)
        {
            case HorizontalAlignment.Left:
                {
                    xPos = _cRt.rect.xMin - _rt.rect.xMin;
                    break;
                }
            case HorizontalAlignment.Middle:
                {
                    xPos = _cRt.rect.center.x;
                    break;
                }
            case HorizontalAlignment.Right:
                {
                    xPos = _cRt.rect.xMax - _rt.rect.xMax;
                    break;
                }
        }

        return xPos;
    }

    private static float GetVerticalAlignment(VerticalAlignment _vAlignment, ref RectTransform _rt)
    {
        Canvas _c = _rt.parent.GetComponent<Canvas>();
        if (_c == null)
        {
            Debug.LogError("No Canvas!");
            return 0;
        }

        RectTransform _cRt = _c.GetComponent<RectTransform>();

        float yPos = 0;
        switch (_vAlignment)
        {
            case VerticalAlignment.Bottom:
                {
                    yPos = _cRt.rect.yMin - _rt.rect.yMin;
                    break;
                }
            case VerticalAlignment.Middle:
                {
                    yPos = _cRt.rect.center.y;
                    break;
                }
            case VerticalAlignment.Top:
                {
                    yPos = _cRt.rect.yMax - _rt.rect.yMax;
                    break;
                }
        }

        return yPos;
    }
}
