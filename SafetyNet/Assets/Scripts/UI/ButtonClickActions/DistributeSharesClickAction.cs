using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistributeSharesClickAction : BaseClickAction
{
    protected override void OnButtonClicked()
    {
        GameManager.Instance.DistributeSharesEvenly();
    }
}
