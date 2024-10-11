using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// A UI component that allows the card to be dragged and dropped. This is not the card itself, this is where the card sits on the table
/// </summary>
public class UI_CardSlot : MonoBehaviour
{
    Card _cardInSlot;
    [SerializeField] private Image _childCardImage; //child image that stores the card sprite
    public bool HasCardInSlot()
    {
        if (_cardInSlot == null) return false;
        return true; 
    }
    public void SetCardInSlot(Card card)
    {
        _cardInSlot = card;
        _childCardImage.sprite = card.GetCardSprite(); 
    }
    public void RemoveCardFromSlot()
    {
        _cardInSlot = null;
        _childCardImage.sprite =null;
    }
}
