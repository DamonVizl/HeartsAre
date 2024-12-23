using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardCardState : BaseState<PlayState>
{
    PlayStateMachine _stateMachine; 
    public DiscardCardState(PlayStateMachine sm, PlayState key) : base(key)
    {
        _stateMachine = sm; 
    }

    public override void EndTurn()
    {
        throw new System.NotImplementedException();
    }

    public override void EnterState()
    {
        GameManager.Instance.Get_uiPlayerHand().DisablePlayerHandInteractionBtns();
        //show a message to discard cards
        UI_MessageManager.Instance.ShowMessage("Select any cards you don't want, and click the bin"); 
        //enable the discard button

    }

    public override void ExitState()
    {
        
    }

    public override PlayState GetNextState()
    {
        return PlayState.PlayerTurn; 
    }

    public override void UpdateState()
    {
    }
}
