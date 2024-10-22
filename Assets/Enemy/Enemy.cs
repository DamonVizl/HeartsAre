using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy
{
    private static int _minDamage = 1;
    private static int _maxDamage = 10;
    private static int _minNumAttacks = 1;
    private static int _maxNumAttacks = 3;

    private static UI_HeartDefender _heartDefenderRef;
    private static UI_DamageUpdater _ui_DamageUpdater;

    private static float _damageMultiplier = 1f;
    private static float _damageIncreasePerTurn = .1f;
    private static float _numAttacksMultiplier = 1f;
    private static float _numAttacksIncreasePerTurn = .1f;

    public static event Action<int> OnDamageCalculated;

    public Enemy(UI_HeartDefender heartDefenderRef, UI_DamageUpdater damageUpdater)
    {
        _heartDefenderRef = heartDefenderRef;
        _ui_DamageUpdater = damageUpdater;
    }

    // calls the attack method in UI_DamageUpdater so coroutines can be used for player reaction between attacks
    public static void Attack(int value)
    {
        _heartDefenderRef.EnableOptionsForEnemyAttack();
        _ui_DamageUpdater.StartAttack(value);
        Debug.Log("UI_DamageUpdater is attacking with the number of attacks set in the GM instance");
    }

    // calculates a random number of attacks for this turn
    public static int CalculateNumOfAttacks()
    {
        int randomNumAttacks = UnityEngine.Random.Range(_minNumAttacks, _maxNumAttacks);
        int scaledNumOfAttacks = Mathf.RoundToInt(randomNumAttacks * _numAttacksMultiplier);

        return scaledNumOfAttacks;
    }

    // calculate a random amount of damage for an attack
    public static int CalculateDamage()
    {
        int randomDamage = UnityEngine.Random.Range(_minDamage, _maxDamage);
        int scaledDamage = Mathf.RoundToInt(randomDamage * _damageMultiplier); // scale the damage to the current turn
        return scaledDamage;
    }

    // call this at the end of each enemy turn to increase the damage for the next round
    public void IncreaseDamage()
    {
        _damageMultiplier += _damageIncreasePerTurn;
    }

    public void IncreaseNumberOfAttacks()
    {
        _numAttacksMultiplier += _numAttacksIncreasePerTurn;
    }


}
