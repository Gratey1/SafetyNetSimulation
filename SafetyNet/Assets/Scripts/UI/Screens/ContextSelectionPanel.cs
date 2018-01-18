using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ContextSelectionPanel : MonoBehaviour
{
    public delegate void ContextSelectionHandler(ContextSelectionPanel _csp);
    public event ContextSelectionHandler OnSelected;
    public void TriggerOnSelected() { if (!IsLocked && OnSelected != null) OnSelected(this); }

    public Image BgImg;
    private Button btn;
    public Text TheText;
    public object TheObject;
    public bool IsLocked;
    public Color LockedColor;
    private Color startColor;

    private void Awake()
    {
        startColor = BgImg.color;
        btn = BgImg.GetComponent<Button>();
    }

    public void Setup(string _text, object _theObject, bool _isLocked)
    {
        TheText.text = _text;
        TheObject = _theObject;
        IsLocked = _isLocked;
        if(btn != null)
        {
            btn.enabled = !IsLocked;
        }
        BgImg.color = IsLocked ? LockedColor : startColor;
    }
}