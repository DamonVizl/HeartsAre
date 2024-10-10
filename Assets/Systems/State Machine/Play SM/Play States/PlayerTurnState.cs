using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnState : BaseState<PlayState> 
{
    PlayStateMachine _stateMachine; //hold a reference to the statemachine 
    public PlayerTurnState(PlayStateMachine sm, PlayState key) : base(key)
    {
        _stateMachine = sm;
    }

    public override void EnterState()
    {
        Debug.Log("Entering Player Turn state. This is where the player will draw cards, play rummy hands and set themselves up for the enemy's turn");
        //update UI to show that it's the players turn
        //enable player control
        
    }

    public override void ExitState()
    {
        //remove player control
    }

    public override PlayState GetNextState()
    {
        //if the player 
        return PlayState.PlayerTurn;
    }

    public override void UpdateState()
    {
        //await the player confirming they are done, then move to the play state. 
    }
}
