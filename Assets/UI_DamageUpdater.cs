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
            //UpdateDamageUI(dmg);
            StartCoroutine(AnimateDamageText(dmg));
            defenderToAttack.TakeDamage(dmg);
            yield return new WaitForSeconds(2f);
        }

        ResetDamageText();
    }

    private IEnumerator AnimateDamageText(int damage)
    {
        int currentDisplayValue = 0;
        int finalDamageValue = damage;
        float duration = .5f; // Time to complete the counting
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            currentDisplayValue = Mathf.FloorToInt(Mathf.Lerp(0, finalDamageValue, elapsedTime / duration));
            _damageTMP.text = currentDisplayValue.ToString();
            yield return null;
        }

        yield return new WaitForSeconds(.5f);

        // Ensure it shows the exact final damage value at the end
        _damageTMP.text = finalDamageValue.ToString();
    }

    private void ResetDamageText()
    {
        UpdateDamageUI(0);
    }
}
