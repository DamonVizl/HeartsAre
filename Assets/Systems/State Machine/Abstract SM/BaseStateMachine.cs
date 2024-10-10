using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base State Machine. concrete state machines will extend this. 
/// </summary>
/// <typeparam name="EState"></typeparam>
public abstract class BaseStateMachine<EState> : MonoBehaviour where EState : Enum
{
    //this is a dictionary of states that the state machine will have. you'll be able to get the state by calling the dictionary with it's State Enum (EState)
    protected Dictionary<EState, BaseState<EState>> States = new Dictionary<EState, BaseState<EState>>();
    //This is the statemachines current state
    protected BaseState<EState> CurrentState;// 
    private void Start()
    {
        CurrentState.EnterState();
    }
    private void Update()
    {
        EState nextStateKey = CurrentState.GetNextState();
        if (nextStateKey.Equals(CurrentState.StateKey))
        {
            CurrentState.UpdateState();
        }
        else
        {
            TransitionToState(nextStateKey);
        }
    }
    public virtual void TransitionToState(EState stateKey)
    {
        CurrentState.ExitState();
        CurrentState = States[stateKey];
        CurrentState.EnterState();
    }

}
