using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]

public class UI_PlayerCurrencyUpdater : MonoBehaviour
{
    TextMeshProUGUI _currencyTMP;

    void OnEnable()
    {
        _currencyTMP = GetComponent<TextMeshProUGUI>();
        CurrencyManager.OnCurrencyChanged += UpdateCurrencyUI;
    }

    private void OnDisable()
    {
        CurrencyManager.OnCurrencyChanged -= UpdateCurrencyUI;
    }

    private void UpdateCurrencyUI(int money)
    {
        _currencyTMP.text = money.ToString();
    }


}
