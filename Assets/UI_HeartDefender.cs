using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HeartDefender : MonoBehaviour
{
    public GameObject heartDefenderPrefab;
    public GameObject addHeartDefenderPrefab;

    private List<GameObject> heartDefenders = new List<GameObject>();

    public Transform cardContainer;

    public int addDefenderCost;

    [SerializeField] private int maxDefenders;


    public void BuyDefender()
    {
        if (heartDefenders.Count < maxDefenders)
        {
            if (CurrencyManager.GetCurrentMoney() >= addDefenderCost)
            {
                AddDefender();
            }
            else
            {
                Debug.Log("not enough money");
            }
        }
        else
        {
            Debug.Log("you've reached the max limit of defenders");
        }
    }

    private void AddDefender()
    {
        CurrencyManager.RemoveMoney(addDefenderCost);
        GameObject currentPlaceHolder = cardContainer.GetChild(cardContainer.childCount - 1).gameObject;

        SwapWithDefender(currentPlaceHolder);

        AddNewPlaceHolder();

    }


    private void SwapWithDefender(GameObject placeholder)
    {
        GameObject newDefender = Instantiate(heartDefenderPrefab, placeholder.transform.position, Quaternion.identity);
        newDefender.transform.SetParent(cardContainer, false);

        heartDefenders.Add(newDefender);

        Destroy(placeholder);
    }

    private void AddNewPlaceHolder()
    {
        GameObject newPlaceholder = Instantiate(addHeartDefenderPrefab, cardContainer);

    }








}
