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
    private HeartDefender selectedDefender; // defender player chooses to defend with

    private EnemyTurnState _enemyTurnState;

    private void OnEnable()
    {
        _damageTMP = GetComponent<TextMeshProUGUI>();
        Enemy.OnDamageCalculated += UpdateDamageUI;

    }

    private void OnDisable()
    {
        Enemy.OnDamageCalculated -= UpdateDamageUI;
    }

    public void SetEnemyTurnState(EnemyTurnState enemyTurnState)
    {
        _enemyTurnState = enemyTurnState;
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
            Debug.Log("Waiting for player to pick a defender");
            yield return StartCoroutine(WaitForPlayerToSelectDefender());
            int dmg = Enemy.CalculateDamage();

            StartCoroutine(AnimateDamageText(dmg));
            selectedDefender.TakeDamage(dmg);
            yield return new WaitForSeconds(2f);
        }


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

    private IEnumerator WaitForPlayerToSelectDefender()
    {
        bool defenderSelected = false;

        void OnDefenderSelected(HeartDefender defender)
        {
            selectedDefender = defender;
            defenderSelected = true;
        }

        GameManager.OnDefenderSelected += OnDefenderSelected;

        while (!defenderSelected)
        {
            yield return null;
        }

        GameManager.OnDefenderSelected -= OnDefenderSelected;
        Debug.Log("player has picked a defender to absorb the damage");
    }
}
