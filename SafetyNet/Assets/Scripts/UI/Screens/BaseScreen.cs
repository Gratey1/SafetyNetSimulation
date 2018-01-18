using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public delegate void GenericHandler();

public class BaseScreen : MonoBehaviour
{
    public event GenericHandler GoBack;
    public virtual void OnGoBackSelected() { if (GoBack != null) GoBack(); }

    [SerializeField]
    private ScreenType type;

    [SerializeField]
    private RectTransform screenRectTransform;

    public ScreenType Type { get { return type; } }

    protected virtual void Awake()
    {

    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
        //screenRectTransform.localScale = new Vector3(1, 1, 1);
        //rectTransform.position = new Vector3(0, 0, 0);
        //screenRectTransform.sizeDelta = new Vector2(0, 0);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }

    public virtual void Initialize()
    {

    }

    public virtual void Shutdown()
    {

    }
}