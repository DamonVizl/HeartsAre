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

    private UI_HeartDefender _ui_heartDefender;

    private void Start()
    {
        _ui_heartDefender = FindObjectOfType<UI_HeartDefender>();
    }

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

    public void StartAttack(int numberOfAttacks, List<HeartDefender> defendersForThisAttack)
    {
        //StartCoroutine(AttackRoutine(numberOfAttacks));

        StartCoroutine(AttackRoutine(numberOfAttacks, defendersForThisAttack));


    }

    // attack the player a number of times and allow the player to choose a defender between attacks
    private IEnumerator AttackRoutine(int numberOfAttacks, List<HeartDefender> defendersForThisAttack)
    {
        int attacksRemaining = numberOfAttacks;

        while (attacksRemaining > 0)
        {
            if (defendersForThisAttack.Count == 0)
            {
                Debug.Log("No defenders left to attack!");
                yield break; // Stop the coroutine if no defenders are left
            }

            // Pick a random defender
            int randomIndex = UnityEngine.Random.Range(0, defendersForThisAttack.Count);
            HeartDefender randomDefender = defendersForThisAttack[randomIndex];

            // Perform attack on the random defender
            randomDefender.TakeDamage(Enemy.CalculateDamage());
            // Wait for a delay before the next attack
            yield return new WaitForSeconds(_attackDelay);

            attacksRemaining--;
        }

        if(_enemyTurnState != null)
        {
            _enemyTurnState.EndTurn();  // Call EndTurn after attacks
        }
        else
        {
            Debug.LogWarning("EnemyTurnState reference is null, cannot end turn.");
        }
    }

 

    // animates damage total in a rollup number
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

        yield return new WaitForSeconds(.25f);

        // Ensure it shows the exact final damage value at the end
        _damageTMP.text = finalDamageValue.ToString();
    }

    // resets the damage text to 0
    private void ResetDamageText()
    {
        UpdateDamageUI(0);
    }

}
