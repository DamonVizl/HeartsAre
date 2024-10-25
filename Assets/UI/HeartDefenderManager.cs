using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class HeartDefenderManager : MonoBehaviour
{
    public GameObject heartDefenderPrefab;

    private List<HeartDefender> heartDefenders = new List<HeartDefender>();
    private List<HeartDefender> defendersForThisAttack = new List<HeartDefender>();


    public int addDefenderCost;

    public Button addDefenderButton;

    public Transform cardContainer;

    public UI_HeartDefenderInteractions _ui_HeartDefenderInteractions;

    [SerializeField] private const int maxDefenders = 5;
    RectTransform parentRectTransform; // UI parent reference for camera shake

    public static event Action<UI_ConfirmSelection> OnConfirmSelection;


    void Start()
    {
        _ui_HeartDefenderInteractions.GetComponent<UI_HeartDefenderInteractions>();
        // assign the listener to the starting addHeartDefender UI element
        addDefenderButton.onClick.AddListener(BuyDefender);
        parentRectTransform = GetComponent<RectTransform>().parent.GetComponent<RectTransform>();
        CheckForStartingDefenders(); // check if there are any heart defenders in play at start of game and add to heartDefenders  
    }

    public void PlayerConfirmsSelection(UI_ConfirmSelection confirmSelectionButton)
    {
        Enemy.Attack();
    }

    // checks if there are any heart defenders in play and adds them to the heartDefenders list - should be used at Start()
    void CheckForStartingDefenders()
    {
        HeartDefender[] startingDefenders = FindObjectsOfType<HeartDefender>();

        for (int i = 0; i < startingDefenders.Length; i++)
        {
            heartDefenders.Add(startingDefenders[i]);
        }
    }


    // if player has enough $ and hasn't exceeded the defender limit, add a new defender
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
                ShakeCamera();
            }
        }
        else
        {
            Debug.Log("you've reached the max limit of defenders");
        }
    }

    // add a new defender by swapping it with the addDefender placeholder and then move the buy UI element to the next slot down
    private void AddDefender()
    {
        CurrencyManager.RemoveMoney(addDefenderCost);
        GameObject currentPlaceHolder = cardContainer.GetChild(cardContainer.childCount - 1).gameObject;

        AddNewDefender(currentPlaceHolder);
    }


    // swap the buy UI element with the heart defender
    private void AddNewDefender(GameObject placeholder)
    {
        GameObject newDefenderObj = Instantiate(heartDefenderPrefab, cardContainer);
        // update index in hierarchy so add defender option is moved to the right
        int currentIndex = transform.GetSiblingIndex();
        newDefenderObj.transform.SetSiblingIndex(currentIndex);

        HeartDefender heartDefender = newDefenderObj.GetComponent<HeartDefender>();

        AddToDefenderList(heartDefender);
    }

    public void AddToDefenderList(HeartDefender defender)
    {
        heartDefenders.Add(defender);
    }

    public void RemoveFromDefenderList(HeartDefender defender)
    {
        heartDefenders.Remove(defender);
    }


    private void ShakeCamera()
    {
        CameraShake cameraShake = Camera.main.GetComponent<CameraShake>();

        if (cameraShake != null && parentRectTransform != null)
        {
            cameraShake.StartCameraShake(.2f, .5f, parentRectTransform); // shake camera to signal unable to perform action
        }
    }

    public List<HeartDefender> getHeartDefenderList()
    {
        return heartDefenders;
    }


    public void AddDefenderForAttack(HeartDefender defender)
    {
        defendersForThisAttack.Add(defender);
    }

    public void RemoveDefenderForAttack(HeartDefender defender)
    {
        defendersForThisAttack.Remove(defender);
    }

    public void ClearDefenseList()
    {
        defendersForThisAttack.Clear();
    }

    public List<HeartDefender> getDefendersForThisAttack()
    {
        return defendersForThisAttack;
    }

   












}
