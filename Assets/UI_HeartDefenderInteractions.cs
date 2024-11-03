using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_HeartDefenderInteractions : MonoBehaviour
{
    public GameObject defenderOptions_UI_Container;
    public GameObject noDefenderOption_UI;
    public GameObject addDefenderObj_UI;
    public GameObject endTurnButton;

    public TextMeshProUGUI defenderCounter;

    [SerializeField] private HorizontalLayoutGroup layoutGroup;


    // Start is called before the first frame update
    void Start()
    {
        DisableNoDefenderOption();
        layoutGroup = GetComponent<HorizontalLayoutGroup>();
    }
    private void OnEnable()
    {
        PlayerTurnState.OnPlayerTurnStateEnter += EnableOptionsForPlayerTurn; 
    }
    private void OnDisable()
    {
        PlayerTurnState.OnPlayerTurnStateEnter -= EnableOptionsForPlayerTurn;

    }
    public void DisableNoDefenderOption()
    {
        noDefenderOption_UI.SetActive(false);
    }

    public void EnableOptionsForEnemyAttack()
    {
        EnableConfirmSelection_Button();
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

    public void UpdateInventoryCounter(int remainingInventory) => defenderCounter.text = remainingInventory.ToString();
}
