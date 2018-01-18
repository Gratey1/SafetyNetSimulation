using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ContextSelectionScreen : BaseScreen 
{
    public class ContextOption
    {
        public string Text;
        public object Data;
        public bool IsLocked;

        public ContextOption(string _text, object _data, bool _isLocked = false)
        {
            Text = _text;
            Data = _data;
            IsLocked = _isLocked;
        }
    }

    public delegate void ContextSelectionHandler(object _object);
    public event ContextSelectionHandler OnSelected;
    private void TriggerOnSelected(object _object) { if (OnSelected != null) OnSelected(_object); }

    [SerializeField]
    private RectTransform screenRect;

    [SerializeField]
    private Text header;

    [SerializeField]
    private ContextSelectionPanel panelPrefab;
    private List<ContextSelectionPanel> panelInstances = new List<ContextSelectionPanel>();

    [SerializeField]
    private RectTransform gridParent;

    [SerializeField]
    private Button backBtn;

    private List<ContextOption> options;

    public void Initialize(string _header, List<ContextOption> _options, bool showBackBtn = false, HorizontalAlignment _hAlign = HorizontalAlignment.Middle, VerticalAlignment _vAlign = VerticalAlignment.Middle)
    {
        header.text = _header;
        options = _options;
        backBtn.gameObject.SetActive(showBackBtn);
        SetupPanels();
        Align(_hAlign, _vAlign);
    }

    public override void Shutdown()
    {
        base.Shutdown();

        foreach (ContextSelectionPanel _csp in panelInstances)
        {
            _csp.OnSelected -= OnSelectionMade;
        }
    }

    public void Align(HorizontalAlignment _h, VerticalAlignment _v)
    {
        screenRect.AlignOnScreen(_h, _v);
    }

    private void SetupPanels()
    {
        if (options == null) return;

        int _i = 0;
        foreach(ContextOption _option in options)
        {
            ContextSelectionPanel _csp;
            if(_i >= panelInstances.Count)
            {
                _csp = Instantiate(panelPrefab);
                _csp.transform.SetParent(gridParent);
                panelInstances.Add(_csp);
            }
            else
            {
                _csp = panelInstances[_i];
                _csp.gameObject.SetActive(true);
            }
            _csp.Setup(_option.Text, _option.Data, _option.IsLocked);
            _csp.OnSelected += OnSelectionMade;

            ++_i;
        }

        while(_i < panelInstances.Count)
        {
            panelInstances[_i++].gameObject.SetActive(false);
        }
    }

    void OnSelectionMade(ContextSelectionPanel _csp)
    {
        TriggerOnSelected(_csp.TheObject);
    }
}
