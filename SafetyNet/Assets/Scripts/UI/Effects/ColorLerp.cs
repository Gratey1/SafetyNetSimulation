using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ColorLerp : MonoBehaviour
{
    //--------------------------------------------------------------------

    public enum State
    {
        AtDefault,
        MovingToTarget,
        AtTarget,
        MovingToDefault,
    }

    //--------------------------------------------------------------------

    public delegate void StateChangedHandler(State _newState);
    public event StateChangedHandler StateChanged;
    private void TriggerStateChanged(State _newState) { if (StateChanged != null) StateChanged(_newState); }

    //--------------------------------------------------------------------

    [SerializeField]
    private Image image;

    [SerializeField]
    private Color targetColor;
    private Color defaultColor;

    [SerializeField]
    private float duration = 1.0f;
    private float curTime = 0.0f;

    private State curState = State.AtDefault;

    //--------------------------------------------------------------------

    void Awake()
    {
        enabled = false;
        defaultColor = image.color;
    }

    void Update()
    {
        switch (curState)
        {
            case State.MovingToDefault:
                {
                    curTime -= Time.deltaTime;
                    if (curTime <= 0.0f)
                    {
                        curTime = 0.0f;
                        curState = State.AtDefault;
                        TriggerStateChanged(curState);
                        enabled = false;
                    }
                    break;
                }
            case State.MovingToTarget:
                {
                    curTime += Time.deltaTime;
                    if (curTime <= duration)
                    {
                        curTime = duration;
                        curState = State.AtTarget;
                        TriggerStateChanged(curState);
                        enabled = false;
                    }
                    break;
                }
        }

        float t = (duration < 0.001f) ? 0.0f : (curTime / duration);
        image.color = Color.Lerp(defaultColor, targetColor, t);
    }

    public void MoveToTarget()
    {
        if (curState == State.MovingToTarget || curState == State.AtTarget) return;
        curState = State.MovingToTarget;
        TriggerStateChanged(curState);
        enabled = true;
    }

    public void MoveToDefault()
    {
        if (curState == State.MovingToDefault || curState == State.AtDefault) return;
        curState = State.MovingToDefault;
        TriggerStateChanged(curState);
        enabled = true;
    }

    //--------------------------------------------------------------------
}
