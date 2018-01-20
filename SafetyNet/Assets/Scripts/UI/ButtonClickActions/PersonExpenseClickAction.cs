using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonExpenseClickAction : BaseClickAction
{
    [SerializeField]
    private PersonDisplay personDisplay;

    [SerializeField]
    private float amount;

    protected override void OnButtonClicked()
    {
        if (personDisplay == null || personDisplay.Person == null)
            return;

        personDisplay.Person.RemoveValue(amount);
    }
}
