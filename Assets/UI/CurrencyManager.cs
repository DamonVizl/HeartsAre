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

    public static void AutoGenerateCurrency()
    {
        int randomAmount = UnityEngine.Random.Range(0, 100);
        AddMoney(randomAmount);
    }

    public static void AddMoney(int value)
    {
        _currentMoney += value;
        OnCurrencyChanged?.Invoke(_currentMoney);
    }

    public static void RemoveMoney(int value)
    {
        if (_currentMoney >= value)
        {
            _currentMoney -= value;
            OnCurrencyChanged?.Invoke(_currentMoney);
        }
        else
        {
            Debug.Log("not enough money for this action");
        }
    }

    public static int GetCurrentMoney()
    {
        return _currentMoney;
    }



}
