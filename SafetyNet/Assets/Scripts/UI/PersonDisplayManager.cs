using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonDisplayManager : OurMonoBehaviour
{
    [SerializeField]
    private Transform displayParent;

    [SerializeField]
    private PersonDisplay personDisplayPrefab;
    private List<PersonDisplay> displayInstances;

    private void Awake()
    {
        displayInstances = new List<PersonDisplay>();
    }

    public override void OnEnable()
    {
        base.OnEnable();

        GameManager.PeopleAdded += OnPeopleAdded;
        GameManager.PersonAdded += OnPersonAdded;

        GameManager.PeopleRemoved += OnPeopleRemoved;
        GameManager.PersonRemoved += OnPersonRemoved;
    }

    public override void OnDisable()
    {
        base.OnDisable();

        GameManager.PeopleAdded -= OnPeopleAdded;
        GameManager.PersonAdded -= OnPersonAdded;

        GameManager.PeopleRemoved -= OnPeopleRemoved;
        GameManager.PersonRemoved -= OnPersonRemoved;
    }

    public PersonDisplay GetPersonDisplay(Person _person)
    {
        if(displayInstances != null)
        {
            return displayInstances.Find(_p => (_p.Person != null) && (_p.Person.ID == _person.ID));
        }

        return null;
    }

    private void OnPeopleAdded(Person[] _people)
    {
        if (_people == null)
            return;

        for(int i = 0; i < _people.Length; i++)
        {
            OnPersonAdded(_people[i]);
        }  
    }

    private void OnPersonAdded(Person _person)
    {
        if (_person == null)
            return;

        PersonDisplay _display = Instantiate<PersonDisplay>(personDisplayPrefab, displayParent);
        _display.Initialize(_person);

        displayInstances.Add(_display);
    }

    private void OnPeopleRemoved(Person[] _people)
    {
        if (_people == null)
            return;

        for(int i = 0; i < _people.Length; i++)
        {
            OnPersonRemoved(_people[i]);
        }
    }

    private void OnPersonRemoved(Person _person)
    {
        if (_person == null)
            return;

        PersonDisplay _display = GetPersonDisplay(_person);
        if (_display == null)
            return;

        _display.Shutdown();
        displayInstances.Remove(_display);
        this.SafeDestroy(_display.gameObject);
    }
}
