using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonDisplay : OurMonoBehaviour
{
    // ****************************************************************************************

    [SerializeField]
    private Image iconImg;


    [Header("Name")]

    [SerializeField]
    private Text nameTxt;

    [SerializeField]
    private Color maleNameColor = Color.blue;

    [SerializeField]
    private Color femaleNameColor = Color.magenta;


    [Header("Value")]

    [SerializeField]
    private Text valueTxt;

    [SerializeField]
    private string formattableValueStr = "Value: ${0}";

    [SerializeField]
    private Color positiveValueColor = Color.green;

    [SerializeField]
    private Color negativeValueColor = Color.red;


    [Header("Portfolio")]

    [SerializeField]
    private Text portfolioTxt;

    [SerializeField]
    private string formattablePortfolioStr = "Portfolio: ${0}";

    [SerializeField]
    private Color positivePortfolioColor = Color.green;

    [SerializeField]
    private Color negativePortfolioColor = Color.red;

    private Person person;
    private Coroutine portfolioCalculationCoroutine;
    private float lastPortfolioValue;

    // ****************************************************************************************

    public Person Person { get { return person; } }
        
    // ****************************************************************************************

    public void Initialize(Person _person)
    {
        person = _person;
        if (person == null)
            return;

        person.ValueUpdated += OnValueUpdated;

        portfolioCalculationCoroutine = StartCoroutine(PortfolioCalculationCoroutine());
        lastPortfolioValue = 0.0f;

        iconImg.sprite = person.Icon;
        UpdateNameTxt();
        UpdateValueTxt();
        UpdatePortfolioTxt();
    }

    public void Shutdown()
    {
        if (person == null)
            return;

        person.ValueUpdated -= OnValueUpdated;

        if(portfolioCalculationCoroutine != null)
        {
            StopCoroutine(portfolioCalculationCoroutine);
            portfolioCalculationCoroutine = null;
        }

        person = null;
    }

    private void UpdateNameTxt()
    {
        Color _color = (person.BiologicalSex == EBiologicalSex.Male) ? maleNameColor : femaleNameColor;
        nameTxt.color = _color;

        nameTxt.text = person.Name;
    }

    private void UpdateValueTxt()
    {
        UpdateTxt(valueTxt, formattableValueStr, person.PersonalValue, positiveValueColor, negativeValueColor);
    }

    private void UpdatePortfolioTxt()
    {
        UpdateTxt(portfolioTxt, formattablePortfolioStr, lastPortfolioValue, positivePortfolioColor, negativePortfolioColor);
    }

    private void UpdateTxt(Text _txt, string _formattableStr, float _num, Color _positiveColor, Color _negativeColor)
    {
        Color _color = (_num > 0.0001f) ? _positiveColor : _negativeColor;
        _txt.color = _color;

        string _numStr = _num.ToString("0.00");
        string _formattedStr = string.Format(_formattableStr, _numStr);
        _txt.text = _formattedStr;
    }

    private IEnumerator PortfolioCalculationCoroutine()
    {
        while (true)
        {
            int _framesToSkip = UnityEngine.Random.Range(0, 60);
            for (int i = 0; i < _framesToSkip; i++)
            {
                yield return null;
            }

            lastPortfolioValue = GameManager.Instance.CalculatePortfolioValue(person);
        }
    }

    private void OnValueUpdated(float _num)
    {
    }

    // ****************************************************************************************
}
