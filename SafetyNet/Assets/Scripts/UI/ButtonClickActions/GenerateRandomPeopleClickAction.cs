using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateRandomPeopleClickAction : BaseClickAction
{
    [SerializeField]
    private Text numPeopleTxt;

    protected override void OnButtonClicked()
    {
        int _numPeople = 0;
        if (numPeopleTxt == null || !int.TryParse(numPeopleTxt.text, out _numPeople))
            return;

        GameManager.Instance.PersonGenerator.GeneratePeople(_numPeople);
    }
}
