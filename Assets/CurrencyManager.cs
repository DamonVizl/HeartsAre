using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager
{
    private static int _currentMoney;
    public static event Action<int> OnCurrencyChanged;

    // Start is called before the first frame update
    public CurrencyManager(int startingValue)
    {
        _currentMoney = startingValue;
        OnCurrencyChanged?.Invoke(_currentMoney);
    }

    public void AddMoney(int value)
    {
        _currentMoney += value;
        OnCurrencyChanged?.Invoke(_currentMoney);
    }

    public static void RemoveMoney(int value)
    {
        if (_currentMoney >= value)
        {
            _currentMoney -= value;
        }
        else
        {
            Debug.Log("not enough money for this action");
        }
    }



}
