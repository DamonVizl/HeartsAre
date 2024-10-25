using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_ConfirmSelection : MonoBehaviour
{
    public Button button;

    public HeartDefenderManager _heartDefenderManager;

    private void Start()
    {
        _heartDefenderManager = FindObjectOfType<HeartDefenderManager>();
        button.onClick.AddListener(OnSelected);
    }

    private void OnSelected()
    {
        //GameManager.PlayerSelectsNoDefender(this);
        if (_heartDefenderManager != null)
        {
            _heartDefenderManager.PlayerConfirmsSelection(this);
        }
    }
}
