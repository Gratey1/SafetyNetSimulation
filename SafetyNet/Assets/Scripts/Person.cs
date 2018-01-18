using System.Collections;
using System.Collections.Generic;

// ********************************************************************************************

public enum EBiologicalSex
{
    Male,
    Female
}

// ********************************************************************************************

public class Person
{
    protected static uint NextId = 0;

    // ********************************************************************************************

    public event FloatDelegate ValueAdded;
    public event FloatDelegate ValueRemoved;

    // ********************************************************************************************

    protected uint id;
    protected string name;
    protected EBiologicalSex biologicalSex;
    protected float paycheckAmount;
    protected uint weeksBetweenPay;
    protected float savingsPercentage;
    protected float expensesPercentage;
    protected float weeklyExpenses;
    protected float personalValue;
    protected Dictionary<uint, float> portfolio;

    // ********************************************************************************************

    public uint ID { get { return id; } }
    public string Name { get { return name; } }
    public EBiologicalSex BiologicalSex { get { return biologicalSex; } }
    public float PaycheckAmount { get { return paycheckAmount; } }
    public uint WeeksBetweenPay { get { return weeksBetweenPay; } }
    public float SavingsPercentage { get { return savingsPercentage; } }
    public float ExpensesPercentage { get { return expensesPercentage; } }
    public float WeeklyExpenses { get { return weeklyExpenses; } }
    public float PersonalValue { get { return personalValue; } }
    protected Dictionary<uint, float> Portfolio { get { return portfolio; } }

    // ********************************************************************************************

    public Person(string _name, EBiologicalSex _sex, float _paycheckAmount, float _savingsPercentage, uint _weeksBetweenPay)
    {
        id = Person.NextId++;
        name = _name;
        biologicalSex = _sex;
        paycheckAmount = _paycheckAmount;
        savingsPercentage = _savingsPercentage;
        expensesPercentage = 1.0f - _savingsPercentage;
        weeksBetweenPay = _weeksBetweenPay;

        personalValue = 0.0f;
        portfolio = new Dictionary<uint, float>();

        CalculateWeeklyExpenses();
    }

    public void AddValue(float _amount)
    {
        personalValue += _amount;

        if(ValueAdded != null)
        {
            ValueAdded(_amount);
        }
    }

    public void RemoveValue(float _amount)
    {
        personalValue -= _amount;

        if(ValueRemoved != null)
        {
            ValueRemoved(_amount);
        }
    }

    public void AddToPortfolio(uint _personalId, float _amount)
    {
        if(portfolio.ContainsKey(_personalId))
        {
            portfolio[_personalId] += _amount;
        }
        else
        {
            portfolio.Add(_personalId, _amount);
        }
    }

    public void RemoveFromPortfolio(uint _personalId, float _amount)
    {
        if (portfolio.ContainsKey(_personalId))
        {
            portfolio[_personalId] -= _amount;
            if(portfolio[_personalId] < 0.00000001f)
            {
                portfolio.Remove(_personalId);
            }

        }
    }

    protected void CalculateWeeklyExpenses()
    {
        float _payPerWeek = paycheckAmount / weeksBetweenPay;
        weeklyExpenses = _payPerWeek * expensesPercentage;
    }
    
    // ********************************************************************************************
}
