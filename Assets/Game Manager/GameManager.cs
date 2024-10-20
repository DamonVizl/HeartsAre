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
    private int _startingMoney = 20;
    public int TurnsSurvived { get; private set; } = 0; //the number of turns the player has survived, if it exceeds a threshold the player wins the round
    public int TurnsRequiredToWin { get; private set; } = 4; //how many turns the player must survive to pass the level. 
    public PlayerHealth PlayerHealth { get; private set; } //player's health class. 
    public CurrencyManager CurrencyManager { get; private set; }
    public Enemy Enemy { get; private set; }
    private UI_HeartDefender _ui_heartDefender;
    private UI_DamageUpdater _ui_DamageUpdater;

    private int _attackCount;
#endregion
#region Events
    public static event Action<int> OnTurnUpdated;
    public static event Action<HeartDefender> OnDefenderSelected;
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
        _ui_heartDefender = FindObjectOfType<UI_HeartDefender>();
        _ui_DamageUpdater = FindObjectOfType<UI_DamageUpdater>();
        PlayerHealth = new PlayerHealth(_startingHealth);
        TurnsSurvived = 0;
        OnTurnUpdated?.Invoke(TurnsSurvived);
        CurrencyManager = new CurrencyManager(_startingMoney);
        Enemy = new Enemy(_ui_heartDefender, _ui_DamageUpdater);
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

    public static void PlayerSelectsDefender(HeartDefender heartDefender)
    {
        OnDefenderSelected?.Invoke(heartDefender);
    }

    public int GetNumberOfAttacks()
    {
        int numberOfAttacks = 0; 
        
        numberOfAttacks = Enemy.CalculateNumOfAttacks();
        return numberOfAttacks;
    }

    public void Attack()
    {
        SetAttackCount();
        Debug.Log("GM is calling Enemy class to attack");
        Enemy.Attack(_attackCount);
    }

    public void ReduceAttackCount()
    {
        _attackCount--;
    }

    public int GetNumberOfAttacksLeft()
    {
        return _attackCount;
    }


    public void SetAttackCount()
    {
        _attackCount = GetNumberOfAttacks();
    }

}
