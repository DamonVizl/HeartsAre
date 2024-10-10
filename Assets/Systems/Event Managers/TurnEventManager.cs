using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TurnEventManager : MonoBehaviour
{
    public static TurnEventManager Instance; 

    void Awake()
    {
        //setup singleton
        if (Instance == null) { Instance = this; }
        else Destroy(this);
    }
    public event Action OnPlayerTurnEnded;  
    public void EndPlayerTurn()
    {
        OnPlayerTurnEnded?.Invoke();
    }
}
