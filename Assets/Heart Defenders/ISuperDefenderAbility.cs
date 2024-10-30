using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISuperDefenderAbility
{
    void Activate(SuperDefenderManager value);
    void Deactivate();
    void ApplyPassiveEffect();
}
