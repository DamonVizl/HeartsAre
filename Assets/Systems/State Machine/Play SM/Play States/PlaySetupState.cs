using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySetupState : BaseState<PlayState>
{
    PlayStateMachine _stateMachine; //hold a reference to the statemachine 
    public PlaySetupState(PlayStateMachine sm, PlayState key) : base(key)
    {
        _stateMachine = sm;
    }

    public override void EnterState()
    {
        Debug.Log("Entering Setup state. This is where the player will chose their special cards prior to the game commencing"); 
    }

    public override void ExitState()
    {
        //
    }

    public override PlayState GetNextState()
    {
        return PlayState.PlayerTurn; 
    }

    public override void UpdateState()
    {
        //await the player confirming they are done, then move to the play state. 
    }
}

