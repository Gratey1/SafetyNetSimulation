using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void DefaultDelegate();
public delegate void FloatDelegate(float _num);

public class GameManager : OurMonoBehaviour
{
    // ***************************************************************************

    protected static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    // ***************************************************************************

    #region Events

    public static event PersonDelegate PersonAdded;
    public static event PersonDelegate PersonRemoved;

    public static event PeopleDelegate PeopleAdded;
    public static event PeopleDelegate PeopleRemoved;

    public static event DefaultDelegate PlaySelected;
    public static event DefaultDelegate PauseSelected;
    public static event DefaultDelegate ResetSelected;

    public static event DefaultDelegate WeekUpdated;

    // ***************************************************************************

    private void TriggerPersonAdded(Person _person)
    {
        if (PersonAdded != null)
        {
            PersonAdded(_person);
        }
    }

    private void TriggerPersonRemoved(Person _person)
    {
        if (PersonRemoved != null)
        {
            PersonRemoved(_person);
        }
    }

    private void TriggerPeopleAdded(Person[] _people)
    {
        if (PeopleAdded != null)
        {
            PeopleAdded(_people);
        }
    }

    private void TriggerPeopleRemoved(Person[] _people)
    {
        if (PeopleRemoved != null)
        {
            PeopleRemoved(_people);
        }
    }

    private void TriggerPlaySelected()
    {
        if (PlaySelected != null)
        {
            PlaySelected();
        }
    }

    private void TriggerPauseSelected()
    {
        if (PauseSelected != null)
        {
            PauseSelected();
        }
    }

    private void TriggerResetSelected()
    {
        if (ResetSelected != null)
        {
            ResetSelected();
        }
    }

    private void TriggerWeekUpdated()
    {
        if (WeekUpdated != null)
        {
            WeekUpdated();
        }
    }

    #endregion // Events

    // ***************************************************************************

    [SerializeField]
    private WorldCanvas worldCanvasPrefab;
    private WorldCanvas worldCanvasInstance;

    [SerializeField]
    private PersonGenerator personGeneratorPrefab;
    private PersonGenerator personGeneratorInstance;

    [SerializeField]
    private float secondsPerYear = 10.0f;
    private float secondsPerWeek;
    private float timeTilNextWeek;
    private uint curWeek;

    private List<Person> people;

    private bool isPlaying;

    private Coroutine portfolioDistributionCoroutine;

    // ***************************************************************************

    public WorldCanvas WorldCanvas { get { return worldCanvasInstance; } }
    public float SecondsPerYear { get { return secondsPerYear; } }
    public float SecondsPerWeek { get { return secondsPerWeek; } }
    public float TimeTilNextWeek { get { return timeTilNextWeek; } }
    public uint CurWeek { get { return curWeek; } }
    public List<Person> People { get { return people; } }
    public bool IsPlaying { get { return isPlaying; } }

    // ***************************************************************************

    public void Play()
    {
        if (isPlaying)
            return;

        isPlaying = true;
        TriggerPlaySelected();
    }

    public void Pause()
    {
        if (!isPlaying)
            return;

        isPlaying = false;
        TriggerPauseSelected();
    }

    public void ResetGame()
    {
        TriggerPeopleRemoved(people.ToArray());
        ResetVariables();
        TriggerResetSelected();
    }

    public Person GenerateRandomPerson()
    {
        Person _person = personGeneratorInstance.GeneratePerson();
        AddPerson(_person);
        return _person;
    }

    public Person[] GenerateRandomPeople(int _count)
    {
        Person[] _people = personGeneratorInstance.GeneratePeople(_count);
        AddPeople(_people);
        return _people;
    }

    public void AddPerson(Person _person)
    {
        people.Add(_person);
        TriggerPersonAdded(_person);
    }

    public void RemovePerson(Person _person)
    {
        people.Remove(_person);
        TriggerPersonRemoved(_person);
    }

    public void AddPeople(Person[] _people)
    {
        people.AddArrays(_people);
        TriggerPeopleAdded(_people);
    }

    public void RemovePeople(Person[] _people)
    {
        people.RemoveArrays(_people);
        TriggerPeopleRemoved(_people);
    }
    
    public void DistributeSharesEvenly()
    {
        if(portfolioDistributionCoroutine != null)
        {
            StopCoroutine(portfolioDistributionCoroutine);
        }

        portfolioDistributionCoroutine = StartCoroutine(DistributeSharesEvenlyCoroutine());
    }

    public void DistributePersonsShares(Person _shareGiver, Person[] _recipients, float _percentagePerPerson)
    {
        for(int i = 0; i < _recipients.Length; i++)
        {
            Person _recipient = _recipients[i];
            _recipient.AddToPortfolio(_shareGiver, _percentagePerPerson);
        }
    }

    public float CalculatePortfolioValue(Person _person)
    {
        float _value = 0.0f;
        for (var e = _person.Portfolio.GetEnumerator(); e.MoveNext();)
        {
            // Multiply the share master's value by the percentage of shares owned by _person
            _value += e.Current.Key.PersonalValue * e.Current.Value;
        }

        return _value;
    }

    public void ResetAllPortfolios()
    {
        people.ForEach(_person => _person.ResetPortfolio());
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
        if(GameManager.instance != null)
        {
            Debug.LogError("Multiple instances of GameManager! Destroying duplicate.");
            this.SafeDestroy(this.gameObject);
            return;
        }

        GameManager.instance = this;
        worldCanvasInstance = GameObject.Instantiate<WorldCanvas>(worldCanvasPrefab);
        personGeneratorInstance = GameObject.Instantiate<PersonGenerator>(personGeneratorPrefab);

        ResetVariables();
    }

    private void OnDestroy()
    {
        GameManager.instance = null;

        if (worldCanvasInstance != null)
        {
            this.SafeDestroy(worldCanvasInstance);
            worldCanvasInstance = null;
        }

        if (personGeneratorInstance != null)
        {
            this.SafeDestroy(personGeneratorInstance);
            personGeneratorInstance = null;
        }
    }

    private void ResetVariables()
    {
        isPlaying = false;
        secondsPerWeek = secondsPerYear / 52;
        people = new List<Person>();
        timeTilNextWeek = secondsPerWeek;
        curWeek = 0;

        TriggerWeekUpdated();
    }

    private void UpdateWeek()
    {
        timeTilNextWeek -= Time.deltaTime;
        if (timeTilNextWeek <= 0.0f)
        {
            people.ForEach(_person => ProcessWeek(_person));
            timeTilNextWeek = secondsPerWeek;
            curWeek++;

            TriggerWeekUpdated();
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

    IEnumerator DistributeSharesEvenlyCoroutine()
    {
        Person[] _recipients = people.ToArray();
        float _percentagePerPerson = 1.0f / people.Count;

        ResetAllPortfolios();

        yield return null;
        
        for (int i = 0; i < people.Count; i++)
        {
            Person _person = people[i];
            DistributePersonsShares(_person, _recipients, _percentagePerPerson);
            yield return null;
        }
    }

    // ***************************************************************************
}
