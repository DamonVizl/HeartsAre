using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// menu state is entered on game start, menu UI will show
/// </summary>
public class MenuState : BaseState<PlayState>
{
    PlayStateMachine _stateMachine;
    MenuManager _menuManager;
    public MenuState(PlayStateMachine sm, PlayState key) : base(key)
    {
        _stateMachine = sm;
    }

    public override void EndTurn()
    {
        throw new System.NotImplementedException();
    }

    public override void EnterState()
    {
        //enable menu
        _menuManager.gameObject.SetActive(true);
    }

    public override void ExitState()
    {
        //disable the menu
        _menuManager.gameObject.SetActive(false);
    }

    public override PlayState GetNextState()
    {
        return PlayState.Menu; 
    }

    public override void UpdateState()
    {
       
    }
}
