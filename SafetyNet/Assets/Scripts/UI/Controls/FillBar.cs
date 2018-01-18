using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FillBar : MonoBehaviour 
{
    public enum Direction
    {
        Up,
        Dowm,
        Left,
        Right,
    }

    [SerializeField]
    private Direction direction = Direction.Right;

    [SerializeField]
    private Text valueText;

    [SerializeField]
    private RectTransform fillRectTransform;

    private float normalizedValue = 1.0f;

	void Awake () 
    {
        if (fillRectTransform == null) return;

        SetPivot();
        SetScale();
    }

    public void SetFillValue(float _curValue, float _maxValue)
    {
        float _normalizedFillValue = (_maxValue < 0.001f) ? 0.0f : (_curValue / _maxValue );
        normalizedValue = Mathf.Clamp01(_normalizedFillValue);

        if (valueText != null)
        {
            valueText.text = string.Format("{0} / {1}", _curValue.ToString("0"), _maxValue.ToString("0"));
        }

        SetScale();
    }

    public void SetFillValue(float _normalizedFillValue)
    {
        normalizedValue = Mathf.Clamp01(_normalizedFillValue);

        if(valueText != null)
        {
            valueText.text = (100*normalizedValue).ToString("0") + "%";
        }

        SetScale();
    }

    private void SetPivot()
    {
        if (fillRectTransform == null) return;
        switch (direction)
        {
            case Direction.Up:
                {
                    fillRectTransform.pivot = new Vector2(0.5f, 0.0f);
                    break;
                }
            case Direction.Dowm:
                {
                    fillRectTransform.pivot = new Vector2(0.5f, 1.0f);
                    break;
                }
            case Direction.Left:
                {
                    fillRectTransform.pivot = new Vector2(1.0f, 0.5f);
                    break;
                }
            case Direction.Right:
                {
                    fillRectTransform.pivot = new Vector2(0.0f, 0.5f);
                    break;
                }
        }
    }

    private void SetScale()
    {
        if (fillRectTransform == null) return;

        Vector3 _localScale = fillRectTransform.localScale;
        switch (direction)
        {
            case Direction.Up:
            case Direction.Dowm:
                {
                    _localScale.y = normalizedValue;
                    break;
                }
            case Direction.Left:
            case Direction.Right:
                {
                    _localScale.x = normalizedValue;
                    break;
                }
        }
        fillRectTransform.localScale = _localScale;
    }
}
