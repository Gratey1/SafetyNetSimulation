using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextInputScreen : BaseScreen 
{
    public delegate void SetTextHandler(string _text);
    public event SetTextHandler SetText;

    [SerializeField]
    private Text headerTxt;

    [SerializeField]
    private InputField inputField;

    public void Initialize(string _header, string _defaultText = "")
    {
        headerTxt.text = _header;
        inputField.text = _defaultText;
    }

    public void TriggerSetText()
    {
        if(SetText != null)
        {
            SetText(inputField.text);
        }
    }
}


