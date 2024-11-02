using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenDefenderAbility : ISuperDefenderAbility
{
    public int _healingAmount = 5;


    public void Activate(SuperDefenderManager value)
    {
        Debug.Log("Queen super defender is in play");
        GameManager.Instance.PlayerHealth.AddHealth(_healingAmount);
    }

    public void Deactivate()
    {

    }

    public void ApplyPassiveEffect()
    {
        HealPlayer();
    }

    private void HealPlayer()
    {
        GameManager.Instance.PlayerHealth.AddHealth(_healingAmount);
    }
}
