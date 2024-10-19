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
        int numberOfAttacks = CalculateNumOfAttacks();

        for (int i = 0; i < numberOfAttacks; i++)
        {
            HeartDefender defenderToAttack = GetRandomDefender();
            int dmg = CalculateDamage();

            defenderToAttack.TakeDamage(dmg);
            Debug.Log(dmg);
        }
    }

    private static HeartDefender GetRandomDefender()
    {
        HeartDefender ranDefender = null;
        List<HeartDefender> currentDefenderList = _heartDefenderRef.getHeartDefenderList();

        int numberOfDefenders = currentDefenderList.Count;

        int randomNum = Random.Range(0, numberOfDefenders);

        ranDefender = currentDefenderList[randomNum];

        return ranDefender;
    }

    private static int CalculateNumOfAttacks()
    {
        int randomNumAttacks = 0;
        randomNumAttacks = Random.Range(_minNumAttacks, _maxNumAttacks);

        return randomNumAttacks;
    }

    private static int CalculateDamage()
    {
        int randomDamage = 0;

        randomDamage = Random.Range(_minDamage, _maxDamage);

        return randomDamage;
    }


}
