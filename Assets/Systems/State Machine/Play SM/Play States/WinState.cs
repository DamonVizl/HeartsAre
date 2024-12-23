using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState : BaseState<PlayState>
{

    PlayStateMachine _stateMachine; //hold a reference to the statemachine 
    public WinState(PlayStateMachine sm, PlayState key) : base(key)
    {
        _stateMachine = sm;
    }

    public override void EnterState()
    {
        SceneController.Instance.SetScene(3); 
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

    public override void EndTurn()
    {

    }
}
