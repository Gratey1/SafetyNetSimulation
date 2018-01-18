using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void DefaultDelegate();
public delegate void FloatDelegate(float _num);

public class GameManager : OurMonoBehaviour
{
    // ***************************************************************************

    public delegate void PersonDelegate(Person _person);
    public delegate void PeopleDelegate(Person[] _people);

    public event PersonDelegate PersonAdded;
    public event PersonDelegate PersonRemoved;

    public event PeopleDelegate PeopleAdded;
    public event PeopleDelegate PeopleRemoved;

    public event DefaultDelegate GameReset;

    // ***************************************************************************

    [SerializeField]
    private float secondsPerYear = 10.0f;
    private float secondsPerWeek;
    private float timeTilNextWeek;
    private uint curWeek;

    private List<Person> people;

    private bool isPlaying;

    // ***************************************************************************

    public void Play()
    {
        isPlaying = true;
    }

    public void Pause()
    {
        isPlaying = false;
    }

    public void Reset()
    {
        if(PeopleRemoved != null)
        {
            PeopleRemoved(people.ToArray());
        }

        ResetVariables();

        if (GameReset != null)
        {
            GameReset();
        }
    }

    public void AddPerson(Person _person)
    {
        people.Add(_person);

        if(PersonAdded != null)
        {
            PersonAdded(_person);
        }
    }

    public void RemovePerson(Person _person)
    {
        people.Remove(_person);

        if(PersonRemoved != null)
        {
            PersonRemoved(_person);
        }
    }

    public void AddPeople(Person[] _people)
    {
        people.AddArrays(_people);

        if(PeopleAdded != null)
        {
            PeopleAdded(_people);
        }
    }

    public void RemovePeople(Person[] _people)
    {
        people.RemoveArrays(_people);

        if (PeopleAdded != null)
        {
            PeopleAdded(_people);
        }
    }

    public override void Update()
    {
        base.Update();

        if (!isPlaying)
            return;

        UpdateWeek();
    }

    // ***************************************************************************

    private void Awake()
    {
        ResetVariables();
    }

    private void ResetVariables()
    {
        secondsPerWeek = secondsPerYear / 52;
        people = new List<Person>();
        curWeek = 0;
        timeTilNextWeek = secondsPerWeek;
    }

    private void UpdateWeek()
    {
        timeTilNextWeek -= Time.deltaTime;
        if (timeTilNextWeek <= 0.0f)
        {
            people.ForEach(_person => ProcessWeek(_person));
            curWeek++;
            timeTilNextWeek = secondsPerWeek;
        }
    }
 
    private void ProcessWeek(Person _person)
    {
        bool _isPayWeek = (curWeek % _person.WeeksBetweenPay) == 0;
        if(_isPayWeek)
        {
            _person.AddValue(_person.PaycheckAmount);
        }

        _person.RemoveValue(_person.WeeklyExpenses);
    }

    // ***************************************************************************
}
