using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayStateMachine : BaseStateMachine<PlayState>
{
    //on awake add all the possible states and instantiate them.
    private void Awake()
    {
        States.Add(PlayState.Setup, new PlaySetupState(this, PlayState.Setup));
        States.Add(PlayState.Menu, new MenuState( this, PlayState.Menu));
        States.Add(PlayState.PlayerTurn, new PlayerTurnState(this, PlayState.PlayerTurn));
        States.Add(PlayState.HeartDefenders, new HeartDefenderState(this, PlayState.HeartDefenders));
        //States.Add(PlayState.DiscardCards, new DiscardCardState(this, PlayState.DiscardCards));
        States.Add(PlayState.ChooseSuperDefender, new ChooseSuperDefenderState(this, PlayState.ChooseSuperDefender));
        States.Add(PlayState.EnemyTurn, new EnemyTurnState(this, PlayState.EnemyTurn));
        States.Add(PlayState.Lose, new LoseState(this, PlayState.Lose));
        States.Add(PlayState.Win, new WinState(this, PlayState.Win));
        CurrentState = States[PlayState.Setup];
    }

    private void Start()
    {
        
    }
    public PlayState GetCurrentState()
    {
        return CurrentState.StateKey; 
    }
    public override void TransitionToState(PlayState stateKey)
    {
        base.TransitionToState(stateKey);
        //_stateLabelText.text = stateKey.ToString();
    }

}
public enum PlayState
{
    Menu,
    Setup, 
    PlayerTurn,
    HeartDefenders,
    ChooseSuperDefender,
    //DiscardCards,
    EnemyTurn,
    Lose,
    Win,
}
