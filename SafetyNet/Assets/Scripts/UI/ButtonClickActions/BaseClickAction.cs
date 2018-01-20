using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class BaseClickAction : OurMonoBehaviour
{
    Button btn;

    protected virtual void Awake()
    {
        btn = GetComponent<Button>();
    }

    public override void OnEnable()
    {
        base.OnEnable();

        btn.onClick.AddListener(OnButtonClicked);
    }

    public override void OnDisable()
    {
        base.OnDisable();

        btn.onClick.RemoveListener(OnButtonClicked);
    }

    protected override bool UsesUpdate()
    {
        return false;
    }

    protected abstract void OnButtonClicked();
}
