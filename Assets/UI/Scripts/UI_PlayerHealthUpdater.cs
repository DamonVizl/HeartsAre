using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UI_PlayerHealthUpdater : MonoBehaviour
{
    TextMeshProUGUI _healthTMP; 
    // Start is called before the first frame update
    private void OnEnable()
    {
        _healthTMP = GetComponent<TextMeshProUGUI>();
        PlayerHealth.OnHealthChange += UpdateHealthUI; 
    }

    private void OnDisable()
    {
        PlayerHealth.OnHealthChange -= UpdateHealthUI;
    }
    private void UpdateHealthUI(int health)
    {
        _healthTMP.text = health.ToString();    
    }



}
