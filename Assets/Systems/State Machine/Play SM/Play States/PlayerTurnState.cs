using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnState : BaseState<PlayState> 
{
    PlayStateMachine _stateMachine; //hold a reference to the statemachine 
    public static event Action OnPlayerTurnStateEnter, OnPlayerTurnStateExit;
    public PlayerTurnState(PlayStateMachine sm, PlayState key) : base(key)
    {
        _stateMachine = sm;
    }

    public override void EnterState()
    {
        OnPlayerTurnStateEnter?.Invoke(); 
        //TODO: we shouldn't pull through the gamemanager to do this, maybe an event OnPlayerTurnStateEntered, and OnPlayerTurnStateExit, then listen in the appropriate thing. 
        //GameManager.Instance.Get_uiPlayerHand().EnablePlayerHandInteractionBtns();

        //GameManager.Instance.GetHeartDefenderManager()._ui_HeartDefenderInteractions.EnableOptionsForPlayerTurn();
        Debug.Log("Entering Player Turn state. This is where the player will draw cards, play rummy hands and set themselves up for the enemy's turn");
        //update UI to show that it's the players turn
        //enable player control
        UI_Button_EndPlayerTurn.OnEndTurnButtonPressed += EndTurn; // subscribe end turn function to End Turn button

        //reset the number of tricks and number of hand refills 
        GameManager.Instance.TricksPlayedThisTurn = 0; 
        GameManager.Instance.DiscardsThisTurn = 0;
        
    }

    public override void ExitState()
    {
        OnPlayerTurnStateExit?.Invoke();
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
        _stateMachine.TransitionToState(PlayState.HeartDefenders); 
/*        // Check if the player has drawn at least one card
        if (GameManager.Instance.GetPlayerHandClass().GetCurrentNumberOfCardsInCollection() > 0)
        {
            _stateMachine.TransitionToState(PlayState.DiscardCards);
        }
        else
        {
            Debug.Log("Player must draw at least one card before ending their turn.");
        }*/
    }

}
