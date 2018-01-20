using System.Collections.Generic;
using UnityEngine;

// ********************************************************************************************

public enum EBiologicalSex
{
    Male,
    Female
}

// ********************************************************************************************

public delegate void PersonDelegate(Person _person);
public delegate void PeopleDelegate(Person[] _people);

// ********************************************************************************************

public class Person
{
    protected static uint NextId = 0;

    // ********************************************************************************************

    #region Events

    public event FloatDelegate ValueUpdated;
    public event DefaultDelegate PortfolioUpdated;

    private void TriggerValueUpdated(float _delta)
    {
        if(ValueUpdated != null)
        {
            ValueUpdated(_delta);
        }
    }

    private void TriggerPortfolioUpdated()
    {
        if(PortfolioUpdated != null)
        {
            PortfolioUpdated();
        }
    }

    #endregion // Events

    // ********************************************************************************************

    protected uint id;
    protected string name;
    protected Sprite icon;
    protected EBiologicalSex biologicalSex;
    protected float paycheckAmount;
    protected uint weeksBetweenPay;
    protected float savingsPercentage;
    protected float expensesPercentage;
    protected float weeklyExpenses;
    protected float personalValue;
    protected Dictionary<Person, float> portfolio;

    // ********************************************************************************************

    public uint ID { get { return id; } }
    public string Name { get { return name; } }
    public Sprite Icon { get { return icon; } }
    public EBiologicalSex BiologicalSex { get { return biologicalSex; } }
    public float PaycheckAmount { get { return paycheckAmount; } }
    public uint WeeksBetweenPay { get { return weeksBetweenPay; } }
    public float SavingsPercentage { get { return savingsPercentage; } }
    public float ExpensesPercentage { get { return expensesPercentage; } }
    public float WeeklyExpenses { get { return weeklyExpenses; } }
    public float PersonalValue { get { return personalValue; } }
    public Dictionary<Person, float> Portfolio { get { return portfolio; } }

    // ********************************************************************************************

    public Person(string _name, Sprite _icon, EBiologicalSex _sex, float _paycheckAmount, float _savingsPercentage, uint _weeksBetweenPay)
    {
        id = Person.NextId++;
        name = _name;
        icon = _icon;
        biologicalSex = _sex;
        paycheckAmount = _paycheckAmount;
        savingsPercentage = _savingsPercentage;
        expensesPercentage = 1.0f - _savingsPercentage;
        weeksBetweenPay = _weeksBetweenPay;

        personalValue = 0.0f;
        portfolio = new Dictionary<Person, float>();

        CalculateWeeklyExpenses();
    }

    public void AddValue(float _amount)
    {
        personalValue += _amount;
        TriggerValueUpdated(_amount);
    }

    public void RemoveValue(float _amount)
    {
        personalValue -= _amount;
        TriggerValueUpdated(-_amount);
    }

    public void AddToPortfolio(Person _shareMaster, float _amount)
    {
        if(portfolio.ContainsKey(_shareMaster))
        {
            portfolio[_shareMaster] += _amount;
        }
        else
        {
            portfolio.Add(_shareMaster, _amount);
        }

        TriggerPortfolioUpdated();
    }

    public void RemoveFromPortfolio(Person _shareMaster, float _amount)
    {
        if (portfolio.ContainsKey(_shareMaster))
        {
            portfolio[_shareMaster] -= _amount;
            if(portfolio[_shareMaster] < 0.00000001f)
            {
                portfolio.Remove(_shareMaster);
            }

            TriggerPortfolioUpdated();
        }
    }

    public void ResetPortfolio()
    {
        portfolio.Clear();
        TriggerPortfolioUpdated();
    }

    protected void CalculateWeeklyExpenses()
    {
        float _payPerWeek = paycheckAmount / weeksBetweenPay;
        weeklyExpenses = _payPerWeek * expensesPercentage;
    }
    
    // ********************************************************************************************
}
