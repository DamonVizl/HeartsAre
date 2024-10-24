using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_ConfirmSelection : MonoBehaviour
{
    public Button button;

    public UI_HeartDefender _ui_HeartDefender;

    private void Start()
    {
        _ui_HeartDefender = FindObjectOfType<UI_HeartDefender>();
        button.onClick.AddListener(OnSelected);
    }

    private void OnSelected()
    {
        //GameManager.PlayerSelectsNoDefender(this);
        if (_ui_HeartDefender != null)
        {
            _ui_HeartDefender.PlayerConfirmsSelection(this);
        }
    }
}
