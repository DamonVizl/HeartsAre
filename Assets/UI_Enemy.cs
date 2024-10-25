using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Enemy : MonoBehaviour
{
    Animator _anim; 
    private void OnEnable()
    {
        UI_DamageUpdater.OnAttackExecuted += AnimateAttack;
        _anim = GetComponent<Animator>();  
    }
    private void OnDisable()
    {
        UI_DamageUpdater.OnAttackExecuted -= AnimateAttack;
    }

    private void AnimateAttack()
    {
        _anim.SetTrigger("Attack");
    }
}
