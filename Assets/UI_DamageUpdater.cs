using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]

public class UI_DamageUpdater : MonoBehaviour
{

    TextMeshProUGUI _damageTMP;

    public float _attackDelay = .5f;

    private void OnEnable()
    {
        _damageTMP = GetComponent<TextMeshProUGUI>();
        Enemy.OnDamageCalculated += UpdateDamageUI;

    }

    private void OnDisable()
    {
        Enemy.OnDamageCalculated -= UpdateDamageUI;
    }

    private void UpdateDamageUI(int dmg)
    {
        _damageTMP.text = dmg.ToString();
    }

    public void StartAttack(int numberOfAttacks)
    {
        StartCoroutine(AttackRoutine(numberOfAttacks));
    }

    private IEnumerator AttackRoutine(int numberOfAttacks)
    {
        for (int i = 0; i < numberOfAttacks; i++)
        {
            HeartDefender defenderToAttack = Enemy.GetRandomDefender();
            int dmg = Enemy.CalculateDamage();
            //Enemy.OnDamageCalculated?.Invoke(dmg);
            UpdateDamageUI(dmg);
            defenderToAttack.TakeDamage(dmg);
            yield return new WaitForSeconds(_attackDelay);
        }

        ResetDamageText();
    }

    private void ResetDamageText()
    {
        UpdateDamageUI(0);
    }
}
