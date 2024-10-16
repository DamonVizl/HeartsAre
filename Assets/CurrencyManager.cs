using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager
{
    private int _currentMoney;
    public static event Action<int> OnCurrencyChanged;

    // Start is called before the first frame update
    public CurrencyManager(int startingValue)
    {
        _currentMoney = startingValue;
        OnCurrencyChanged?.Invoke(_currentMoney);
    }


}
