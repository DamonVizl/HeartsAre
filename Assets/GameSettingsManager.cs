using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingsManager : MonoBehaviour
{
    //public int _startingHP;
    public int _startingMoney;
    //public int _turnsRequiredToWin;

    public int GetStartingMoney()
    {
        return _startingMoney;
    }
}
