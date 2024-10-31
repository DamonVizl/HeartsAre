using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingDefenderAbility : ISuperDefenderAbility
{
    private SuperDefenderManager _superDefenderManager;

    public void Activate(SuperDefenderManager sdm)
    {
        Debug.Log("King super defender is in play");
        _superDefenderManager = sdm;
        ApplyPassiveEffect();
        
    }

    public void Deactivate()
    {

    }

    public void ApplyPassiveEffect()
    {
        List<HeartDefender> activeDefenders = _superDefenderManager.GetHeartDefenderManager().getHeartDefenderList();

        foreach (var heartDefender in activeDefenders)
        {
            heartDefender.CheckAndShowBuffer();
        }

    }
}
