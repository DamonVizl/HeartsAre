using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    #region Fields
    public static GameManager Instance;
    [SerializeField] private int _startingHealth = 25;
    [SerializeField] private int _startingMoney = 30;
    public int TurnsSurvived { get; private set; } = 0; //the number of turns the player has survived, if it exceeds a threshold the player wins the round
    [field: SerializeField] public int TurnsRequiredToWin { get; private set; } = 20; //how many turns the player must survive to pass the level. 
    private int _discardsThisTurn = 0; //this field tracks how many times the player has drawn cards 
    public int DiscardsThisTurn
    {
        get { return _discardsThisTurn; }
        set
        {
            _discardsThisTurn = value;
            OnCardsDiscardedValueChanged?.Invoke(value);
        }
    }
    public int MaxDiscardsPerTurn
    { get; private set; } = 3; //max number of draws (hand refills) per turn
    public static event Action<int> OnCardsDiscardedValueChanged;  //an event to sub to to change UI
    private int _tricksPlayedThisTurn = 0;//how many tricks have been played this turn

    public int TricksPlayedThisTurn
    {
        get { return _tricksPlayedThisTurn; }
        set
        {
            _tricksPlayedThisTurn = value;
            OnTrickPlayedValueChanged?.Invoke(value);
        }
    }

    public int MaxNumberOfTricksPlayablePerTurn { get; private set; } = 3; //max number of tricks playable in a turn
    public static event Action<int> OnTrickPlayedValueChanged; //an event to sub to to change the UI
    public PlayerHealth PlayerHealth { get; private set; } //player's health class. 
    public CurrencyManager CurrencyManager { get; private set; }
    public Enemy _enemy;
    private HeartDefenderManager _heartDefenderManager;
    private UI_HeartDefenderInteractions _ui_HeartDefenderActions;
    private UI_Enemy _ui_enemy;
    private SuperDefenderManager _superDefenderManager;
    private UI_PlayerHand _ui_playerHand;
    private PlayerHand _playerHand;
    private EnemyTurnState _enemyTurnState;
    public float _attackDelay;
    #endregion


    #region Events
    public static event Action<int> OnTurnUpdated;
    public static event Action<int> OnDamageCalculated;
    public static event Action<HeartDefender> OnAttackExecuted;
    #endregion
    #region SetupMethods
    public void Awake()
    {
        //setup singleton
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(this); 
        }
        else Destroy(this);
    }
    private void Start()
    {
        _heartDefenderManager = FindObjectOfType<HeartDefenderManager>();
        _ui_enemy = FindObjectOfType<UI_Enemy>();
        _ui_HeartDefenderActions = FindObjectOfType<UI_HeartDefenderInteractions>();
        _superDefenderManager = FindObjectOfType<SuperDefenderManager>();
        PlayerHealth = new PlayerHealth(_startingHealth);
        // _ui_playerHand = FindObjectOfType<UI_PlayerHand>();
        TurnsSurvived = 0;
        OnTurnUpdated?.Invoke(TurnsSurvived);
        CurrencyManager = new CurrencyManager(_startingMoney);
        _enemy = FindObjectOfType<Enemy>();
        _playerHand = FindObjectOfType<PlayerHand>();
    }
    #endregion


    //progress to the next turn/round
    public void NextTurn()
    {
        TurnsSurvived++;
        if (TurnsSurvived >= TurnsRequiredToWin)
        {
            //Win! 
        }
        OnTurnUpdated?.Invoke(TurnsSurvived);
    }

    //reduce the player's health by x
    public void ReducePlayerHealth(int amount)
    {
        PlayerHealth.RemoveHealth(amount);
    }

    public HeartDefenderManager GetHeartDefenderManager()
    {
        return _heartDefenderManager;
    }

    public SuperDefenderManager GetSuperDefenderManager()
    {
        return _superDefenderManager;
    }

    public UI_PlayerHand Get_uiPlayerHand()
    {
        return _ui_playerHand;
    }

    public PlayerHand GetPlayerHandClass()
    {
        return _playerHand;
    }


    public void StartAttack(int numberOfAttacks, List<HeartDefender> defendersForThisAttack)
    {
        StartCoroutine(AttackRoutine(numberOfAttacks, defendersForThisAttack));
    }

    // attack the player a number of times and allow the player to choose a defender between attacks
    private IEnumerator AttackRoutine(int numberOfAttacks, List<HeartDefender> defendersForThisAttack)
    {
        int attacksRemaining = numberOfAttacks;

        while (attacksRemaining > 0)
        {
            int dmg = Enemy.Instance.CalculateDamage();
            HeartDefender randomDefender = null;
            if (defendersForThisAttack == null || defendersForThisAttack.Count == 0)
            {
                // No defenders available, so attack the player directly

                GameManager.Instance.ReducePlayerHealth(dmg);
                Debug.Log("No defenders available, attacking player for " + dmg + " damage.");
                SFXManager.Instance.PlayRandomSound(SFXName.PlayerTakeDamage);
            }
            else
            {
                // Pick a random defender
                int randomIndex = UnityEngine.Random.Range(0, defendersForThisAttack.Count);
                randomDefender = defendersForThisAttack[randomIndex];

                // Perform attack on the random defender
                randomDefender.TakeDamage(dmg);
                //randomDefender.ShowDamage(dmg, _attackDelay);
            }
            OnAttackExecuted?.Invoke(randomDefender);
            // Wait for a delay before the next attack
            yield return new WaitForSeconds(_attackDelay);

            attacksRemaining--;
        }

        foreach (var defender in defendersForThisAttack)
        {
            defender.ResetPosition();
        }


        // After all attacks, end the enemy turn
        if (_enemyTurnState != null)
        {
            _enemyTurnState.EndTurn();  // Call EndTurn after attacks
        }
        else
        {
            Debug.LogWarning("EnemyTurnState reference is null, cannot end turn.");
        }
    }

    public void SetEnemyTurnState(EnemyTurnState enemyTurnState)
    {
        _enemyTurnState = enemyTurnState;
    }






}
