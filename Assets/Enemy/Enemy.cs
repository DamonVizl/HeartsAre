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

    public static event Action<int> OnDamageCalculated;

    public Enemy(UI_HeartDefender heartDefenderRef, UI_DamageUpdater damageUpdater)
    {
        _heartDefenderRef = heartDefenderRef;
        _ui_DamageUpdater = damageUpdater;
    }

    public static void Attack()
    {
        // get a random number of attacks to use this turn
        int numberOfAttacks = CalculateNumOfAttacks();

        _ui_DamageUpdater.StartAttack(numberOfAttacks);

    }

    // get the current list of defenders and return a random defender
    public static HeartDefender GetRandomDefender()
    {
        HeartDefender ranDefender = null;
        List<HeartDefender> currentDefenderList = _heartDefenderRef.getHeartDefenderList();

        int numberOfDefenders = currentDefenderList.Count;

        int randomNum = UnityEngine.Random.Range(0, numberOfDefenders);

        ranDefender = currentDefenderList[randomNum];

        return ranDefender;
    }

    // calculates a random number of attacks for this turn
    public static int CalculateNumOfAttacks()
    {
        int randomNumAttacks = 0;
        randomNumAttacks = UnityEngine.Random.Range(_minNumAttacks, _maxNumAttacks);

        return randomNumAttacks;
    }

    // calculate a random amount of damage for an attack
    public static int CalculateDamage()
    {
        int randomDamage = 0;

        randomDamage = UnityEngine.Random.Range(_minDamage, _maxDamage);

        return randomDamage;
    }


}
