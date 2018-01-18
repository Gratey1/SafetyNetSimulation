using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SKStateMachine<T>
{
    protected T _context;
#pragma warning disable
    public event Action OnStateChanged;
#pragma warning restore

    public SKState<T> CurrentState { get { return _currentState; } }

    public SKState<T> PreviousState;
    public SKState<T> NextState;
    public float ElapsedTimeInState = 0f;
    private Dictionary<System.Type, SKState<T>> _states = new Dictionary<System.Type, SKState<T>>();
    private SKState<T> _priorState;
    private SKState<T> _currentState;

    public SKStateMachine(T context, SKState<T> initialState)
    {
        this._context = context;

        // setup our initial state
        AddState(initialState);
        PreviousState = initialState;
        _currentState = initialState;
        NextState = initialState;
        _currentState.Begin();
    }


    /// <summary>
    /// adds the state to the machine
    /// </summary>
    public void AddState(SKState<T> state)
    {
        state.SetMachineAndContext(this, _context);
        _states[state.GetType()] = state;
    }


    /// <summary>
    /// ticks the state machine with the provided delta time
    /// </summary>
    public void Update(float deltaTime)
    {
        ElapsedTimeInState += deltaTime;
        _currentState.Update(deltaTime);
    }

    /// <summary>
    /// changes the current state
    /// </summary>
    public R ChangeState<R>() where R : SKState<T>
    {
        // avoid changing to the same state
        var newType = typeof(R);
        if (_currentState.GetType() == newType)
            return _currentState as R;

        if (!_states.ContainsKey(newType))
        {
            var state = Activator.CreateInstance(newType) as SKState<T>;

            state.SetMachineAndContext(this, _context);
            _states.Add(newType, state);
        }

        // only call end if we have a currentState
        if (_currentState != null)
        {
            NextState = _states[newType];
            _currentState.End();
        }

        // swap states and call begin
        PreviousState = _currentState;
        _currentState = _states[newType];
        _currentState.Begin();
        ElapsedTimeInState = 0f;

        // fire the changed event if we have a listener
        if (OnStateChanged != null)
            OnStateChanged();

        return _currentState as R;
    }
}
