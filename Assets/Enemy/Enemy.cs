using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public static Enemy Instance;

    private int _minDamage = 1;
    private int _maxDamage = 5;
    private int _minNumAttacks = 1;
    private int _maxNumAttacks = 2;

    private HeartDefenderManager _heartDefenderManager;
    private UI_Enemy _ui_enemy;

    private float _damageMultiplier = 1f;

    private int _turnCounter = 0;
    private int _turnsToIncreaseDmg = 2; // Number of turns after which to increase values
    private int _turnsToIncreaseAttacks = 4;

    private int _nextAttackCount;

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
        _nextAttackCount = 1;
    }



    // Calls the attack method in UI_DamageUpdater so coroutines can be used for player reaction between attacks
    public void Attack()
    {
        List<HeartDefender> defendersForThisAttack = _heartDefenderManager.getDefendersForThisAttack();
        StartAttack(_nextAttackCount, defendersForThisAttack);

        // Increment turn counter 
        UpdateTurnCounter();
        // check if values need to be increased
        UpdateAttackPower();
    }

    public void StartAttack(int value, List<HeartDefender> defenders)
    {
        _heartDefenderManager._ui_HeartDefenderInteractions.EnableOptionsForEnemyAttack();
        GameManager.Instance.GetUI_DamageUpdater().StartAttack(value, defenders);
    }

    // Calculates a random number of attacks for this turn
    public int CalculateNumOfAttacks()
    {
        int randomNumAttacks = UnityEngine.Random.Range(_minNumAttacks, _maxNumAttacks);

        return randomNumAttacks;
    }

    // Calculate a random amount of damage for an attack
    public int CalculateDamage()
    {
        int randomDamage = UnityEngine.Random.Range(_minDamage, _maxDamage);
        int scaledDamage = Mathf.RoundToInt(randomDamage * _damageMultiplier); // scale the damage to the current turn
        Debug.Log("Damage: " + scaledDamage);
        return scaledDamage;
    }

    // Increase min and max values after every 2 turns
    private void IncreaseDamage()
    {
        _maxDamage += 1;
        Debug.Log($"Values increased! MinDamage: {_minDamage}, MaxDamage: {_maxDamage}, MinNumAttacks: {_minNumAttacks}, MaxNumAttacks: {_maxNumAttacks}");

    }
    private void IncreaseNumberOfAttacks()
    {
        _minNumAttacks += 1;
        _maxNumAttacks += 1;
        Debug.Log($"Values increased! MinDamage: {_minDamage}, MaxDamage: {_maxDamage}, MinNumAttacks: {_minNumAttacks}, MaxNumAttacks: {_maxNumAttacks}");

    }


    public HeartDefenderManager GetHeartDefenderManager()
    {
        return _heartDefenderManager;
    }

    public void UpdateTurnCounter()
    {
        _turnCounter++;
    }

    public void UpdateAttackPower()
    {
        if (_turnCounter % _turnsToIncreaseAttacks == 0)
        {
            IncreaseNumberOfAttacks();
        }

        if (_turnCounter % _turnsToIncreaseDmg == 0)
        {
            IncreaseDamage();
        }
    }

    public void SetNumberOfAttacks(int numAttacksNextRound)
    {
        _nextAttackCount = numAttacksNextRound;
    }

    public int GetNextAttackCount()
    {
        return _nextAttackCount;
    }

}
