using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnState : BaseState<PlayState>
{
    PlayStateMachine _stateMachine; //hold a reference to the statemachine 

    protected int _numberOfAttacks;
    protected int _attackCount = 0;


    public EnemyTurnState(PlayStateMachine sm, PlayState key) : base(key)
    {
        _stateMachine = sm;

    }

    public override void EnterState()
    {
        GameManager.Instance.GetUI_DamageUpdater().SetEnemyTurnState(this);
        GameManager.Instance.GetUI_HeartDefender().EnableOptionsForEnemyAttack();
        Debug.Log("Entering Enemy Turn state. This is where the enemy will do damage to the players cards. The player can't do anything for now.");
        //show some UI to say that it's the enemy's turn

        //Do damage
        //temporary player health reduction. make something a little more sophisticated than this. Also need to encorporate the hearts defence part. 
        //I think I'll make this an event and then the playerhand or whatever can react to it by reducing hearts or taking damage, depending on how many hearts are left.
     
        GameManager.Instance.Attack(); // calls enemy attack that chooses heart defenders at random to attack
    }

    public override void ExitState()
    {
        //
    }

    public override PlayState GetNextState()
    {

        // if the player has no health yet, return lose state. 
        if (!GameManager.Instance.PlayerHealth.IsAlive())
        {
            return PlayState.Lose;
        }

        //if the number of turns has exceeded the amount required to win, return win state. 
        else if (GameManager.Instance.TurnsSurvived >= GameManager.Instance.TurnsRequiredToWin)
        {
            return PlayState.Win;
        }

 
        return PlayState.EnemyTurn;

    }

    public override void UpdateState()
    {
        //await the player confirming they are done, then move to the play state. 
    }

    public void EndTurn()
    {
        _stateMachine.TransitionToState(PlayState.PlayerTurn);

    }

}
