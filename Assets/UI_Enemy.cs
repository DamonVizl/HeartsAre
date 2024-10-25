using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Enemy : MonoBehaviour
{
    Animator _anim;
    private HeartDefender _targetDefender;

    private void OnEnable()
    {
        UI_DamageUpdater.OnAttackExecuted += AnimateAttack;
        _anim = GetComponent<Animator>();  
    }
    private void OnDisable()
    {
        UI_DamageUpdater.OnAttackExecuted -= AnimateAttack;
    }

    private void AnimateAttack(HeartDefender defender)
    {
        _targetDefender = defender;
        _anim.SetTrigger("Attack");
    }

    public void TriggerParticleEffect()
    {
        if (_targetDefender != null)
        {
            _targetDefender.PlayParticleEffect();
        }
    }

}
