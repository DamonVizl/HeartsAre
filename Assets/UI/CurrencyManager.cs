using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager
{
    private static int _currentMoney;
    public static event Action<int> OnCurrencyChanged;

    private static float _currencyMultiplier = 1.5f;

    // Start is called before the first frame update
    public CurrencyManager(int startingValue)
    {
        _currentMoney = startingValue;
        OnCurrencyChanged?.Invoke(_currentMoney);
        CardManager.OnTrickScore += AddMoney; //when a trick is scored, it's value is added to the currency manager
    }

    public static void AddMoney(int value)
    {
       value = Mathf.RoundToInt(value * CurrencyMultiplier());
      

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

    private static float CurrencyMultiplier()
    {
        float _currencyMultiplierTotal = 1f;
        int numberOfJackSuperDefenders = GameManager.Instance.GetSuperDefenderManager().GetJackDefenders().Count;

        if (numberOfJackSuperDefenders > 0)
        {
            _currencyMultiplierTotal = _currencyMultiplier * numberOfJackSuperDefenders;
        }

        return _currencyMultiplierTotal;
    }



}
