using UnityEngine;
using System.Collections;

public class SmoothMove : MonoBehaviour 
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
    private Vector3 direction;

    [SerializeField]
    private float distance = 1.0f;
    
    [SerializeField]
    private float duration = 0.5f;
    private float curTime = 0.0f;
    
    private State curState = State.AtDefault;
    private Vector3 defaultPos;
    private Vector3 targetPos;

    //--------------------------------------------------------------------

	void Awake () 
    {
        enabled = false;
        defaultPos = transform.position;
        targetPos = defaultPos + (direction * distance);
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
        transform.position = Vector3.Lerp(defaultPos, targetPos, t);
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
