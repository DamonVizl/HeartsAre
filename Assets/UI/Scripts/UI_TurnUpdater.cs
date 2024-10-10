using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]

public class UI_TurnUpdater : MonoBehaviour
{
    TextMeshProUGUI _turnTMP;
    // Start is called before the first frame update
    private void OnEnable()
    {
        _turnTMP = GetComponent<TextMeshProUGUI>();
        GameManager.OnTurnUpdated += UpdateTurnUI;
    }

    private void OnDisable()
    {
        GameManager.OnTurnUpdated -= UpdateTurnUI;
    }
    private void UpdateTurnUI(int turn)
    {
        _turnTMP.text = turn.ToString() + " / " + GameManager.Instance.TurnsRequiredToWin;  //updates the UI label with the current turn / turns required to win the round
    }

}
