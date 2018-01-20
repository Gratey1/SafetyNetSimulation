using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRandomPersonClickAction : BaseClickAction
{
    protected override void OnButtonClicked()
    {
        GameManager.Instance.PersonGenerator.GeneratePerson();
    }
}
