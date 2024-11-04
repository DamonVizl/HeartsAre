using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseState : BaseState<PlayState>
{

    PlayStateMachine _stateMachine; //hold a reference to the statemachine 
    public LoseState(PlayStateMachine sm, PlayState key) : base(key)
    {
        _stateMachine = sm;
    }

    public override void EnterState()
    {
        SceneController.Instance.SetScene(2); 
        Debug.Log("Player Lost, return to menu and set next state as Setup for when we are using the PlaySM again");
    }

    public override void ExitState()
    {
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