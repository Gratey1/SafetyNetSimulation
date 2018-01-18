using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlaceholderScreen : BaseScreen 
{
    //**************************************************

    public event GenericHandler ScreenTimerFinished;
    private void TriggerScreenTimerFinished() { if (ScreenTimerFinished != null) ScreenTimerFinished(); }

    //**************************************************

    [SerializeField]
    private Text headerTxt;

    [SerializeField]
    private Text subHeaderTxt;

    private float duration;
    private float curTime;
    private bool isTimerActive;

    //**************************************************

    public void Initialize(float _duration, string _headerText, string _subHeaderText = "")
    {
        headerTxt.text = _headerText;
        subHeaderTxt.text = _subHeaderText;
        
        duration = _duration;
        curTime = 0;

        isTimerActive = duration >= 0;
    }

   protected void Update()
    {
        if (!isTimerActive) return;

       if(curTime >= duration)
       {
           isTimerActive = false;
           TriggerScreenTimerFinished();
       }
       curTime += Time.deltaTime;
    }

    //**************************************************
}
