using System;
using UnityEngine;

public class RemovePersonClickAction : BaseClickAction
{
    [SerializeField]
    private PersonDisplay personDisplay;

    protected override void OnButtonClicked()
    {
        if (personDisplay == null || personDisplay.Person == null)
            return;

        GameManager.Instance.RemovePerson(personDisplay.Person);
    }
}
