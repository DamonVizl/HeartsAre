using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState : BaseState<PlayState>
{

    PlaySM _stateMachine; //hold a reference to the statemachine 
    public WinState(PlaySM sm, PlayState key) : base(key)
    {
        _stateMachine = sm;
    }

    public override void EnterState()
    {
        Debug.Log("Player has won, give rewards. set next state as setup and return to menu's. no longer using the PlaySM for right now.");
    }

    public override void ExitState()
    {
        //
    }

    public override PlayState GetNextState()
    {
        return PlayState.Setup;
    }

    public override void UpdateState()
    { 
    }
}
