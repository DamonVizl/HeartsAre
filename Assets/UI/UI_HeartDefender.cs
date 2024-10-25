using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class UI_HeartDefender : MonoBehaviour
{
    public GameObject heartDefenderPrefab;
    public GameObject defenderOptions_UI_Container;
    public GameObject noDefenderOption_UI;
    public GameObject addDefenderObj_UI;
    public GameObject selectDefenderText;

    public GameObject endTurnButton;

    private List<HeartDefender> heartDefenders = new List<HeartDefender>();
    private List<HeartDefender> defendersForThisAttack = new List<HeartDefender>();

    public Transform cardContainer;

    public int addDefenderCost;

    public Button addDefenderButton;



    [SerializeField] private const int maxDefenders = 5;
    RectTransform parentRectTransform; // UI parent reference for camera shake

    public static event Action<UI_ConfirmSelection> OnConfirmSelection;


    void Start()
    {
        // assign the listener to the starting addHeartDefender UI element
        addDefenderButton.onClick.AddListener(BuyDefender);
        parentRectTransform = GetComponent<RectTransform>().parent.GetComponent<RectTransform>();
        CheckForStartingDefenders(); // check if there are any heart defenders in play at start of game and add to heartDefenders list
        DisableNoDefenderOption();
        selectDefenderText.SetActive(false);
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

    public void DisableNoDefenderOption()
    {
        noDefenderOption_UI.SetActive(false);
    }

    public void EnableOptionsForEnemyAttack()
    {
        DisableAddNewDefender_Button();
        EnableConfirmSelection_Button();
        EnableSelectDefenderText();
        DisableEndTurn_Button();
    }

    public void EnableConfirmSelection_Button()
    {
        noDefenderOption_UI.SetActive(true);
    }

    public void EnableOptionsForPlayerTurn()
    {
        DisableNoDefenderOption();
        EnableAddNewDefenderButton();
        DisableSelectDefenderText();
        EnableEndTurn_Button();
    }

    public void DisableAddNewDefender_Button()
    {
        addDefenderObj_UI.SetActive(false);
    }

    public void EnableAddNewDefenderButton()
    {
        addDefenderObj_UI.SetActive(true);

    }

    public void EnableEndTurn_Button()
    {
        endTurnButton.SetActive(true);
    }

    public void DisableEndTurn_Button()
    {
        endTurnButton.SetActive(false);
    }

    public void EnableSelectDefenderText()
    {
        selectDefenderText.SetActive(true);
    }

    public void DisableSelectDefenderText()
    {
        selectDefenderText.SetActive(false);
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
