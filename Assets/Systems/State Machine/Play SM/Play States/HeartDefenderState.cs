using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartDefenderState : BaseState<PlayState>
{
    private PlayStateMachine _stateMachine;

    public HeartDefenderState(PlayStateMachine sm, PlayState key) : base(key)
    {
        _stateMachine = sm;
    }

    public override void EndTurn()
    {
        throw new System.NotImplementedException();
    }

    public override void EnterState()
    {
        //show message about what to do in this state
        UI_MessageManager.Instance.ShowMessage("Upgrade and select your Heart Defenders for this attack!");
        Enemy.Instance.GetHeartDefenderManager().EnableOptionsForEnemyAttack();
    }

    public override void ExitState()
    {
        
    }

    public override PlayState GetNextState()
    {
        return PlayState.HeartDefenders; 
    }

    public override void UpdateState()
    {
    }
}
