using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class HeartDefender : MonoBehaviour
{
    [SerializeField] private int heartRank;
    [SerializeField] private int heartDmgRate;
    public TextMeshProUGUI rankCounter;
    public int levelUpCost;
    [SerializeField] private GameManager gameManager;
    private PlayerHealth playerHealth;

    public int testDamage;
    public int testMoney;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerHealth = GameManager.Instance.PlayerHealth;
        UpdateRankCounter_UI();

    }

    // updates the badge counter
    public void UpdateRankCounter_UI()
    {
        rankCounter.text = heartRank.ToString();
    }

    // if player tries to purchase a rank upgrade and has enough currency, upgrade the card's rank
    public void PurchaseRankUpgrade(int playerCurrencyAmt)
    {
        int upgradeCost = levelUpCost * heartRank;

        if (playerCurrencyAmt > levelUpCost * heartRank)
        {
            UpgradeRank(upgradeCost);
        }
        else
        {
            Debug.Log("You do not have enough money to upgrade this heart");
        }
    }

    // if heart takes enough damage that surpasses it's rank's damage threshold, decrease its rank
    public void TakeDamage(int damage)
    {
        // track damage amt that surpasses defender's rank
        int overDamage = 0;
        // if damage amount is higher than the defender's, decrease the rank of the heart by the difference between the amount of damage and the heart's rank
        if (damage > heartRank)
        {
            overDamage = damage - heartRank;
            DecreaseRank(overDamage);
        }
    }

    void DecreaseRank(int damage)
    {
        // tracks overkill damage
        int overKillDmg = 0;

        heartRank -= damage;

        overKillDmg = Mathf.Abs(heartRank);

        UpdateRankCounter_UI();

        // if heartRank is 0 or lower, destroy it and apply overkill damage to player
        if (heartRank <= 0)
        {
            DestroyHeart(overKillDmg);
        }
    }

    void UpgradeRank(int cost)
    {
        CurrencyManager.RemoveMoney(cost);
        heartRank++;
        // add SubtractDifferenceToPlayer()
        UpdateRankCounter_UI();
    }

    void OverKillDamage(int damage)
    {
        // if this heart dies, player will receive overkill damage
        playerHealth.RemoveHealth(damage);
    }

    void DestroyHeart(int damage)
    {
        // apply overkill
        OverKillDamage(damage);
        // destroy this heart
        Destroy(this.gameObject);
    }



    private void Update()
    {
        //CheckForTestInputs();
    }


    // just for quick testing of upgrade and decreasing rank - remove
    private void CheckForTestInputs()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            PurchaseRankUpgrade(testMoney);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(testDamage);
        }
    }



}
