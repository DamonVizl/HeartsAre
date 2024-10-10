using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySM : BaseStateMachine<PlayState>
{
    [SerializeField] Text _stateLabelText; 
    //on awake add all the possible states and instantiate them.
    private void Awake()
    {
        States.Add(PlayState.Setup, new PlaySetupState(this, PlayState.Setup));
        States.Add(PlayState.PlayerTurn, new PlayerTurnState(this, PlayState.PlayerTurn));
        States.Add(PlayState.EnemyTurn, new EnemyTurnState(this, PlayState.EnemyTurn));
        States.Add(PlayState.Lose, new LoseState(this, PlayState.Lose));
        States.Add(PlayState.Win, new WinState(this, PlayState.Win));
        CurrentState = States[PlayState.Setup];  
    }
    public PlayState GetCurrentState()
    {
        return CurrentState.StateKey; 
    }
    public override void TransitionToState(PlayState stateKey)
    {
        base.TransitionToState(stateKey);
        _stateLabelText.text = stateKey.ToString();
    }
}
public enum PlayState
{
    Setup,
    PlayerTurn,
    EnemyTurn,
    Lose,
    Win,
}
