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
    private bool playerTakesDamage;

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
            Debug.Log("Waiting for player to pick a defender or take the damage");
            yield return StartCoroutine(WaitForPlayerToSelectDefenderOrPlayer());

            int dmg = Enemy.CalculateDamage();
            StartCoroutine(AnimateDamageText(dmg));

            if (playerTakesDamage)
            {
                // Apply damage directly to the player
                GameManager.Instance.ReducePlayerHealth(dmg); // Assuming you have a Player script handling player health
                Debug.Log("Player takes the damage");
            }
            else
            {
                // Apply damage to the selected defender
                selectedDefender.TakeDamage(dmg);
                Debug.Log("Defender takes the damage");
            }

            yield return new WaitForSeconds(1f);
        }

        // After all attacks, end the enemy turn
        if (_enemyTurnState != null)
        {
            _enemyTurnState.EndTurn();  // Call EndTurn after attacks
        }
        else
        {
            Debug.LogWarning("EnemyTurnState reference is null, cannot end turn.");
        }
    }

    private IEnumerator WaitForPlayerToSelectDefenderOrPlayer()
    {
        bool selectionMade = false;
        playerTakesDamage = false;

        void OnDefenderSelected(HeartDefender defender)
        {
            selectedDefender = defender;
            selectionMade = true;
        }

        void OnNoDefenderSelected(UseNoDefenderOption noDefenderOption)
        {
            playerTakesDamage = true;
            selectionMade = true;
        }

        // Subscribe to events for defender selection and player taking damage
        GameManager.OnDefenderSelected += OnDefenderSelected;
        GameManager.OnNoDefenderSelected += OnNoDefenderSelected;

        // Wait until either the defender is selected or the player chooses to take damage
        while (!selectionMade)
        {
            yield return null;
        }

        // Unsubscribe from the events
        GameManager.OnDefenderSelected -= OnDefenderSelected;
        GameManager.OnNoDefenderSelected -= OnNoDefenderSelected;

        if (playerTakesDamage)
        {
            Debug.Log("Player has chosen to take the damage");
        }
        else
        {
            Debug.Log("Player has picked a defender to absorb the damage");
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

}
