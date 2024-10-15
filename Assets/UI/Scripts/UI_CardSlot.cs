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
    UI_Card _UICard;
    CanvasGroup _cardCanvasGroup; 
    private void OnEnable()
    {
        _UICard = GetComponentInChildren<UI_Card>();
        _cardCanvasGroup = _UICard.GetComponent<CanvasGroup>();
    }
    public bool HasCardInSlot()
    {
        if (_cardInSlot == null) return false;
        return true; 
    }
    public void SetCardInSlot(Card card)
    {
        _cardInSlot = card;
        _UICard.AttachCard(card);
        _cardCanvasGroup.alpha = 1;
        _cardCanvasGroup.blocksRaycasts = true;
        _cardCanvasGroup.interactable = true;
        _UICard.FlipCard();

    }
    public void RemoveCardFromSlot()
    {
        _UICard.RemoveCard(_cardInSlot  );
        _cardInSlot = null;
        _cardCanvasGroup.alpha = 0;
        _cardCanvasGroup.blocksRaycasts = false;
        _cardCanvasGroup.interactable = false;

    }
}
