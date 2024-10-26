using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperDefender : MonoBehaviour
{
    public enum SuperDefenderType
    { 
        Jack,
        Queen,
        King
    }

    public SuperDefenderType superDefenderType;
    private ISuperDefenderAbility superDefenderAbility;

    public void Initialize(SuperDefenderType type)
    {
        this.superDefenderType = type;
        AssignAbility(type);
        superDefenderAbility?.Activate();
    }

    private void AssignAbility(SuperDefenderType type)
    {
        switch (type)
        {
            case SuperDefenderType.Jack:
                superDefenderAbility = new JackDefenderAbility();
                break;
            case SuperDefenderType.Queen:
                superDefenderAbility = new QueenDefenderAbility();
                break;
            case SuperDefenderType.King:
                superDefenderAbility = new KingDefenderAbility();
                break;
        }
    }

    public void ApplyPassiveEffect()
    {
        superDefenderAbility?.ApplyPassiveEffect();
    }

    public SuperDefenderType GetRandomSuperDefenderType()
    {
        SuperDefenderType[] types = { SuperDefenderType.Jack, SuperDefenderType.Queen, SuperDefenderType.King };
        int randomIndex = UnityEngine.Random.Range(0, types.Length);
        return types[1];
    }


}
