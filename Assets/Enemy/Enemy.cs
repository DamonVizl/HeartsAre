using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy
{
    private static int _minDamage = 1;
    private static int _maxDamage = 10;
    private static int _minNumAttacks = 1;
    private static int _maxNumAttacks = 3;

    private static HeartDefenderManager _heartDefenderManager;
    private static UI_DamageUpdater _ui_damageUpdater;
    private static UI_Enemy _ui_enemy;

    private static float _damageMultiplier = 1f;
    private static float _damageIncreasePerTurn = .1f;
    private static float _numAttacksMultiplier = 1f;
    private static float _numAttacksIncreasePerTurn = .1f;

    private static int _turnCounter = 0;
    private static int _turnsToIncrease = 2; // Number of turns after which to increase values

    public static event Action<int> OnDamageCalculated;

    public Enemy(HeartDefenderManager heartDefenderRef, UI_DamageUpdater damageUpdater, UI_Enemy ui_enemy)
    {
        _heartDefenderManager = heartDefenderRef;
        _ui_damageUpdater = damageUpdater;
        _ui_enemy = ui_enemy;
    }

    // Calls the attack method in UI_DamageUpdater so coroutines can be used for player reaction between attacks
    public static void Attack()
    {
        int value = CalculateNumOfAttacks();
        List<HeartDefender> defendersForThisAttack = _heartDefenderManager.getDefendersForThisAttack();
        StartAttack(value, defendersForThisAttack);

        // Increase turn counter and check if values need to be increased
        _turnCounter++;
        if (_turnCounter % _turnsToIncrease == 0)
        {
            IncreaseValues();
        }
    }

    public static void StartAttack(int value, List<HeartDefender> defenders)
    {
        _heartDefenderManager._ui_HeartDefenderInteractions.EnableOptionsForEnemyAttack();
        _ui_damageUpdater.StartAttack(value, defenders);
    }

    // Calculates a random number of attacks for this turn
    public static int CalculateNumOfAttacks()
    {
        int randomNumAttacks = UnityEngine.Random.Range(_minNumAttacks, _maxNumAttacks);
        int scaledNumOfAttacks = Mathf.RoundToInt(randomNumAttacks * _numAttacksMultiplier);

        return scaledNumOfAttacks;
    }

    // Calculate a random amount of damage for an attack
    public static int CalculateDamage()
    {
        int randomDamage = UnityEngine.Random.Range(_minDamage, _maxDamage);
        int scaledDamage = Mathf.RoundToInt(randomDamage * _damageMultiplier); // scale the damage to the current turn
        Debug.Log("Damge: " + scaledDamage);
        return scaledDamage;
    }

    // Increase min and max values after every 2 turns
    private static void IncreaseValues()
    {
        _minDamage += 1;
        _maxDamage += 2; // Increment more for max if desired
        _minNumAttacks += 1;
        _maxNumAttacks += 1;

        Debug.Log($"Values increased! MinDamage: {_minDamage}, MaxDamage: {_maxDamage}, MinNumAttacks: {_minNumAttacks}, MaxNumAttacks: {_maxNumAttacks}");
    }

    public static HeartDefenderManager GetHeartDefenderManager()
    {
        return _heartDefenderManager;
    }
}
