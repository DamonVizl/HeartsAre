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

    public Enemy(UI_HeartDefender heartDefenderRef)
    {
        _heartDefenderRef = heartDefenderRef;
    }

    public static void Attack()
    {
        // get a random number of attacks to use this turn
        int numberOfAttacks = CalculateNumOfAttacks();

        // loop through each attack and target a different defender with random damage with each iteration of the loop
        for (int i = 0; i < numberOfAttacks; i++)
        {
            HeartDefender defenderToAttack = GetRandomDefender();
            int dmg = CalculateDamage();

            defenderToAttack.TakeDamage(dmg);
        }
    }

    // get the current list of defenders and return a random defender
    private static HeartDefender GetRandomDefender()
    {
        HeartDefender ranDefender = null;
        List<HeartDefender> currentDefenderList = _heartDefenderRef.getHeartDefenderList();

        int numberOfDefenders = currentDefenderList.Count;

        int randomNum = Random.Range(0, numberOfDefenders);

        ranDefender = currentDefenderList[randomNum];

        return ranDefender;
    }

    // calculates a random number of attacks for this turn
    private static int CalculateNumOfAttacks()
    {
        int randomNumAttacks = 0;
        randomNumAttacks = Random.Range(_minNumAttacks, _maxNumAttacks);

        return randomNumAttacks;
    }

    // calculate a random amount of damage for an attack
    private static int CalculateDamage()
    {
        int randomDamage = 0;

        randomDamage = Random.Range(_minDamage, _maxDamage);

        return randomDamage;
    }


}
