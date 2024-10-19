using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Button_EndPlayerTurn : MonoBehaviour
{
    public static Action OnEndTurnButtonPressed;

    public void EndTurn()
    {
        OnEndTurnButtonPressed?.Invoke();
    }
}
