using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System; 

public class GameManager : MonoBehaviour
{
    #region Fields
    public static GameManager Instance;
    private int _startingHealth = 5;
    public int TurnsSurvived { get; private set; } = 0; //the number of turns the player has survived, if it exceeds a threshold the player wins the round
    public int TurnsRequiredToWin { get; private set; } = 4; //how many turns the player must survive to pass the level. 
    public PlayerHealth PlayerHealth { get; private set; } //player's health class. 
    #endregion
    #region Events
    public static event Action<int> OnTurnUpdated; 
    #endregion
    #region SetupMethods
    public void OnEnable()
    {
        //setup singleton
        if (Instance == null) Instance = this;
        else Destroy(this);
    }
    private void Start()
    {
        PlayerHealth = new PlayerHealth(_startingHealth);
        TurnsSurvived = 0;
        OnTurnUpdated?.Invoke(TurnsSurvived); 
    }
    #endregion


    //progress to the next turn/round
    public void NextTurn()
    {
        TurnsSurvived++;
        OnTurnUpdated?.Invoke(TurnsSurvived); 
    }

    //reduce the player's health by x
    public void ReducePlayerHealth(int amount)
    {
        PlayerHealth.RemoveHealth(amount);
    }

}
