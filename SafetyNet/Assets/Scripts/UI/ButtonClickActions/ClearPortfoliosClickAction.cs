using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearPortfoliosClickAction : BaseClickAction
{
    protected override void OnButtonClicked()
    {
        GameManager.Instance.ResetAllPortfolios();
    }
}
