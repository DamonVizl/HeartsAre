using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingsManager : MonoBehaviour
{
    public GameSettings gameSettings;
    public int _startingHP;
    public int _startingMoney;
    public int _turnsRequiredToWin;


    // Start is called before the first frame update
    void Start()
    {
        IntializeGameSettings();
    }

    void IntializeGameSettings()
    {
        _startingHP = gameSettings._startingHP;
        _startingMoney = gameSettings._startingMoney;
        _turnsRequiredToWin = gameSettings._turnsRequiredToWin;
    }
}
