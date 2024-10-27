using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HeartDefenderInteractions : MonoBehaviour
{
    public GameObject defenderOptions_UI_Container;
    public GameObject noDefenderOption_UI;
    public GameObject addDefenderObj_UI;
    public GameObject selectDefenderText;

    public GameObject endTurnButton;

    [SerializeField] private HorizontalLayoutGroup layoutGroup;


    // Start is called before the first frame update
    void Start()
    {
        DisableNoDefenderOption();
        selectDefenderText.SetActive(false);
        layoutGroup = GetComponent<HorizontalLayoutGroup>();
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
        EnableLayoutGroup();
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

    public void DisableLayoutGroup()
    {
        if (layoutGroup != null)
        {
            layoutGroup.enabled = false;
        }

    }

    // activates horizontal layout group for defenders
    public void EnableLayoutGroup()
    {
        if (layoutGroup != null)
        {
            layoutGroup.enabled = true;
        }
    }
}
