using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]

public class UI_DamageUpdater : MonoBehaviour
{

    TextMeshProUGUI _damageTMP;

    private void OnEnable()
    {
        _damageTMP = GetComponent<TextMeshProUGUI>();
        Enemy.OnDamageCalculated += UpdateDamageUI;

    }

    private void OnDisable()
    {
        Enemy.OnDamageCalculated += UpdateDamageUI;
    }

    private void UpdateDamageUI(int dmg)
    {
        _damageTMP.text = dmg.ToString();
    }
}
