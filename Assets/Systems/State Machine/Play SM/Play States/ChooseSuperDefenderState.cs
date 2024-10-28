using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseSuperDefenderState : BaseState<PlayState>
{
    PlayStateMachine _stateMachine;

    public ChooseSuperDefenderState(PlayStateMachine sm, PlayState key) : base(key)
    {
        _stateMachine = sm;
    }

    public override void EnterState()
    {
        UI_MessageManager.Instance.ShowMessage("Sacrifice a Jack, Queen, or King to create your super defender");
        Debug.Log("Entering ChooseSuperDefender state. Player is choosing a super defender.");
    }

    public override void ExitState()
    {


    }

    public override PlayState GetNextState()
    {
        return PlayState.ChooseSuperDefender;
    }

    public override void UpdateState()
    {

    }

    public override void EndTurn()
    {
        _stateMachine.TransitionToPreviousState(); // Transition to previous state
    }
}
