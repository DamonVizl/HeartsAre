using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class HeartDefender : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private int heartRank;
    [SerializeField] private int heartDmgRate;
    public TextMeshProUGUI rankCounter;
    public int levelUpCost;

    // updates the badge counter
    public void UpdateRankCounter_UI()
    {
        rankCounter.text = heartRank.ToString();
    }

    // if player tries to purchase a rank upgrade and has enough currency, upgrade the card's rank
    public void PurchaseRankUpgrade(int playerCurrencyAmt)
    {
        if (playerCurrencyAmt > levelUpCost * heartRank)
        {
            UpgradeRank();
        }
        else
        {
            Debug.Log("You do not have enough money to upgrade this heart");
        }
    }

    // if heart takes enough damage that surpasses it's rank's damage threshold, decrease its rank
    public void TakeDamage(int damage)
    {
        if (damage > heartRank * heartDmgRate)
        {
            DecreaseRank();
        }
    }

    void DecreaseRank()
    {
        heartRank--;
        UpdateRankCounter_UI();
    }

    void UpgradeRank()
    {
        heartRank++;
        UpdateRankCounter_UI();
    }

    void CheckForOverKill()
    { 
        // add logic for if this heart dies, player will receive overkill damage
    }

    void DestroyHeart()
    { 
        // add logic for destroying heart
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
            PurchaseRankUpgrade(50);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(25);
        }
    }



}
