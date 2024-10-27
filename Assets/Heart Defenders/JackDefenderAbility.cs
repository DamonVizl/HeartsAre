using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackDefenderAbility : ISuperDefenderAbility
{
    public void Activate()
    {
        Debug.Log("This should activate the Jack Super Defender ability.");
    }

    public void Deactivate()
    {

    }

    public void ApplyPassiveEffect()
    {
        Debug.Log("jack passive affect applied");
    }
}
