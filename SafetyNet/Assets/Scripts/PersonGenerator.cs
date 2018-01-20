using UnityEngine;

public class PersonGenerator : OurMonoBehaviour
{
    // *******************************************************************************************

    [Header("File Paths")]

    [SerializeField]
    private string maleNamesResourcesPath = "Names/MaleNames";

    [SerializeField]
    private string femaleNamesResourcesPath = "Names/FemaleNames";

    [SerializeField]
    private string maleIconsResourcesPath = "Icons/Male";

    [SerializeField]
    private string femaleIconsResourcesPath = "Icons/Female";

    
    [Header("Icons")]

    [SerializeField]
    private Sprite[] maleIcons;

    [SerializeField]
    private Sprite[] femaleIcons;


    [Header("Annual Income")]

    [SerializeField]
    private float minAnnualIncome = 10000f;

    [SerializeField]
    private float maxAnnualIncome = 300000f;

    [SerializeField]
    private AnimationCurve annualIncomeCurve;


    [Header("Savings")]

    [SerializeField]
    private float minSavingsPercentage = 0.05f;

    [SerializeField]
    private float maxSavingsPercentage = 0.3f;

    [SerializeField]
    private AnimationCurve savingsPercentageCurve;


    private string[] maleNames;
    private string[] femaleNames;
    
    private uint[] validWeeksBetweenPay = { 1, 2, 4 };

    // *******************************************************************************************

    public Person[] GeneratePeople(int _num)
    {
        if (_num <= 0)
            return null;

        Person[] _people = new Person[_num];
        for (int i = 0; i < _num; i++)
        {
            _people[i] = GeneratePerson();
        }

        return _people;
    }

    public Person GeneratePerson()
    {
        EBiologicalSex _sex = GetRandomSex();
        string _name = GetRandomName(_sex);
        Sprite _icon = GetRandomIcon(_sex);
        uint _weeksBetweenPay = GetRandomWeeksBetweenPay();
        float _paycheckAmount = (GetRandomAnnualIncome() / 52) * _weeksBetweenPay;
        float _savingsPercentage = GetRandomSavingsPercentage();

        return new Person(_name, _icon, _sex, _paycheckAmount, _savingsPercentage, _weeksBetweenPay);
    }

    // *******************************************************************************************

    private void Awake()
    {
        maleNames = LoadNames(maleNamesResourcesPath);
        femaleNames = LoadNames(femaleNamesResourcesPath);
    }

    private string[] LoadNames(string _path)
    {
        TextAsset _txt = Resources.Load<TextAsset>(_path);
        return _txt.text.Trim().Split(',');
    }

    private string GetRandomName(EBiologicalSex _sex)
    {
        switch(_sex)
        {
            case EBiologicalSex.Male:
                return GetRandomElementInArray<string>(maleNames);
            case EBiologicalSex.Female:
                return GetRandomElementInArray<string>(femaleNames);
            default:
                return "";
        }
    }

    private Sprite GetRandomIcon(EBiologicalSex _sex)
    {
        switch (_sex)
        {
            case EBiologicalSex.Male:
                return GetRandomElementInArray<Sprite>(maleIcons);
            case EBiologicalSex.Female:
                return GetRandomElementInArray<Sprite>(femaleIcons);
            default:
                return null;
        }
    }

    private EBiologicalSex GetRandomSex()
    {
        return EnumExtended.GetRandomEnum<EBiologicalSex>();
    }

    private float GetRandomAnnualIncome()
    {
        return GetRandomValueOnCurve(minAnnualIncome, maxAnnualIncome, annualIncomeCurve);
    }

    private float GetRandomSavingsPercentage()
    {
        return GetRandomValueOnCurve(minSavingsPercentage, maxSavingsPercentage, annualIncomeCurve);
    }

    private uint GetRandomWeeksBetweenPay()
    {
        return GetRandomElementInArray<uint>(validWeeksBetweenPay);
    }

    private T GetRandomElementInArray<T>(T[] _array)
    {
        int _index = UnityEngine.Random.Range(0, _array.Length);
        return _array[_index];
    }

    private float GetRandomValueOnCurve(float _min, float _max, AnimationCurve _curve)
    {
        float _t = _curve.Evaluate(UnityEngine.Random.value);
        return _min + (_t * (_max - _min));
    }

    // *******************************************************************************************
}
