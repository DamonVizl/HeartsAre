using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;


public class MenuManager : MonoBehaviour
{
    PlayStateMachine _stateMachine; 
    private void Start()
    {
        _stateMachine = GameObject.FindObjectOfType<PlayStateMachine>();    
    }

    //call this method from a button in the menu UI
    public void StartGameButtonPressed()
    {
        _stateMachine.TransitionToState(PlayState.PlayerTurn);
        this.gameObject.SetActive(false);
    }

}
