using System;
using UnityEngine;
using UnityEngine.UI;

public class DateDisplay : MonoBehaviour
{
    [SerializeField]
    private Text txt;
    private DateTime startingDateTime;

    private void Awake()
    {
        startingDateTime = DateTime.Now;    
    }

    private void OnEnable()
    {
        GameManager.WeekUpdated += OnWeekUpdated;
    }

    private void OnDisable()
    {
        GameManager.WeekUpdated -= OnWeekUpdated;
    }

    private DateTime GetAdjustedDateTime()
    {
        uint _days = 7 * GameManager.Instance.CurWeek;
        return startingDateTime + new TimeSpan((int)_days, 0, 0, 0, 0);
    }

    private void OnWeekUpdated()
    {
        DateTime _adjustedDateTime = GetAdjustedDateTime();
        txt.text = _adjustedDateTime.ToShortDateString();
    }
}
