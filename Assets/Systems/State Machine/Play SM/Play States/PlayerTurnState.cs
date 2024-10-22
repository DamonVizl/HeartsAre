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
        CurrencyManager.AutoGenerateCurrency(); // auto generates currency, mainly for testing now. remove if player should not have auto-currency
        GameManager.Instance.GetUI_HeartDefender().EnableOptionsForPlayerTurn();
        Debug.Log("Entering Player Turn state. This is where the player will draw cards, play rummy hands and set themselves up for the enemy's turn");
        //update UI to show that it's the players turn
        //enable player control
        UI_Button_EndPlayerTurn.OnEndTurnButtonPressed += EndTurn; // subscribe end turn function to End Turn button
        
    }

    public override void ExitState()
    {
        //remove player control
        UI_Button_EndPlayerTurn.OnEndTurnButtonPressed -= EndTurn; // unsubscribe end turn button functionality
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

    // end turn and switch to enemy turn
    public override void EndTurn()
    {
        _stateMachine.TransitionToState(PlayState.EnemyTurn);
        Debug.Log("transitioning to the enemy's turn");
    }
}
