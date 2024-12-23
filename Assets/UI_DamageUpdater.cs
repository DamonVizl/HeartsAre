using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//[RequireComponent(typeof(TextMeshProUGUI))]

public class UI_DamageUpdater : MonoBehaviour
{
    public static event Action<HeartDefender> OnAttackExecuted;
    [SerializeField] TextMeshProUGUI _damageTMP;

    public float _attackDelay;
    public float _animateSpeed;

    private EnemyTurnState _enemyTurnState;
    private bool playerTakesDamage;

    //private HeartDefenderManager _heartDefenderManager;
    [SerializeField] private TextMeshProUGUI _damageLabelTMP;

    public GameObject attackIconPrefab;
    public Transform attackIconContainer;

    private void Start()
    {
        //_heartDefenderManager = FindObjectOfType<HeartDefenderManager>();

/*        // Initialize _damageTMP in Start
        _damageTMP = GetComponent<TextMeshProUGUI>();

        // Check if _damageLabelTMP is assigned
        if (_damageLabelTMP == null)
        {
            Debug.LogWarning("_damageLabelTMP is not assigned in the inspector.");
        }*/

        HideDamageUI();
    }

    private void OnEnable()
    {
        GameManager.OnDamageCalculated += UpdateDamageUI;
    }

    private void OnDisable()
    {
        GameManager.OnDamageCalculated -= UpdateDamageUI;
    }

    public void HideDamageUI()
    {
        // Check _damageLabelTMP and _damageTMP to avoid null errors
        if (_damageLabelTMP != null)
        {
            _damageLabelTMP.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("_damageLabelTMP is null when attempting to hide it.");
        }

        if (_damageTMP != null)
        {
            _damageTMP.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("_damageTMP is null when attempting to hide it.");
        }
    }
    void ShowDamageUI()
    {
        _damageLabelTMP.gameObject.SetActive(true);
        _damageTMP.gameObject.SetActive(true);
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
        ShowDamageUI();
        StartCoroutine(AttackRoutine(numberOfAttacks, defendersForThisAttack));
    }

    // attack the player a number of times and allow the player to choose a defender between attacks
    private IEnumerator AttackRoutine(int numberOfAttacks, List<HeartDefender> defendersForThisAttack)
    {
        int attacksRemaining = numberOfAttacks;

        while (attacksRemaining > 0)
        {
            int dmg = Enemy.Instance.CalculateDamage();
            HeartDefender randomDefender = null;
            if (defendersForThisAttack == null || defendersForThisAttack.Count == 0)
            {
                // No defenders available, so attack the player directly

                GameManager.Instance.ReducePlayerHealth(dmg);
                Debug.Log("No defenders available, attacking player for " + dmg + " damage.");
                StartCoroutine(AnimateDamageText(dmg));
                ResetDamageText();
                RemoveAttackIcon();
                SFXManager.Instance.PlayRandomSound(SFXName.PlayerTakeDamage);
            }
            else
            {
                // Pick a random defender
                int randomIndex = UnityEngine.Random.Range(0, defendersForThisAttack.Count);
                randomDefender = defendersForThisAttack[randomIndex];

                // Perform attack on the random defender
                randomDefender.TakeDamage(dmg);
                StartCoroutine(AnimateDamageText(dmg));
                //randomDefender.ShowDamage(dmg, _attackDelay);
                ResetDamageText();
                RemoveAttackIcon();
            }
            OnAttackExecuted?.Invoke(randomDefender);
            // Wait for a delay before the next attack
            yield return new WaitForSeconds(_attackDelay);

            attacksRemaining--;
        }

        foreach (var defender in defendersForThisAttack)
        {
            defender.ResetPosition();
        }

        ResetDamageText();

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

    // animates damage total in a rollup number
    private IEnumerator AnimateDamageText(int damage)
    {
        int currentDisplayValue = 0;
        int finalDamageValue = damage;
        float duration = _animateSpeed; // Time to complete the counting
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            currentDisplayValue = Mathf.FloorToInt(Mathf.Lerp(0, finalDamageValue, elapsedTime / duration));
            _damageTMP.text = currentDisplayValue.ToString();
            yield return null;
        }

        yield return new WaitForSeconds(.1f);

        // Ensure it shows the exact final damage value at the end
        _damageTMP.text = finalDamageValue.ToString();
    }

    // resets the damage text to 0
    private void ResetDamageText()
    {
        UpdateDamageUI(0);
    }

    public void ShowAttackIcons(int numberOfAttacks)
    {
        ClearAttackIcons(); // Clear previous icons if necessary

        for (int i = 0; i < numberOfAttacks; i++)
        {
            GameObject attackIcon = Instantiate(attackIconPrefab);
            attackIcon.transform.SetParent(attackIconContainer);
        }
    }

    public void ClearAttackIcons()
    {
        // Logic to clear existing attack icons
        foreach (Transform child in attackIconContainer)
        {
            Destroy(child.gameObject);
        }
    }

    public void RemoveAttackIcon()
    {
        if (attackIconContainer.childCount > 0)
        {
            Transform iconToDestroy = attackIconContainer.GetChild(0);
            Destroy(iconToDestroy.gameObject);
        }
    }

}
