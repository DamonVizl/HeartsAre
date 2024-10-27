using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class UI_ConfirmSelection : MonoBehaviour, IPointerClickHandler
{
    PlayStateMachine _stateMachine;

    private void Start()
    {
        _stateMachine = GameObject.FindObjectOfType<PlayStateMachine>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _stateMachine.TransitionToState(PlayState.EnemyTurn);

    }
}
