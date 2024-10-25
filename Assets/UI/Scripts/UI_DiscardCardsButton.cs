using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// sits on the discard card button and pushes  this card when selected
/// </summary>
public class UI_DiscardCardsButton : MonoBehaviour

{
    public static event Action OnDiscardCardButtonPressed;

    public void ClickDiscardButton()
    {
        OnDiscardCardButtonPressed?.Invoke();
    }
}
