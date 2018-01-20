using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateRandomPeopleClickAction : BaseClickAction
{
    [SerializeField]
    private InputField numPeopleInputField;

    protected override void OnButtonClicked()
    {
        int _numPeople = 0;
        if (numPeopleInputField == null || !int.TryParse(numPeopleInputField.text, out _numPeople))
            return;

        GameManager.Instance.GenerateRandomPeople(_numPeople);
    }
}
