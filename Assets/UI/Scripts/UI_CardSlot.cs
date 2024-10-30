using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;


/// <summary>
/// A UI component that allows the card to be dragged and dropped. This is not the card itself, this is where the card sits on the table
/// </summary>
public class UI_CardSlot : MonoBehaviour
{
    Card _cardInSlot;
    UI_Card _UICard;
    CanvasGroup _cardCanvasGroup;

    Tween _moveTween; 
    private void OnEnable()
    {
        _UICard = GetComponentInChildren<UI_Card>();
        _cardCanvasGroup = _UICard.GetComponent<CanvasGroup>();
    }
    public bool HasCardInSlot(Card card)
    {
        if (_cardInSlot == card) return true;
        return false; 
    }
    public UI_Card GetCardInSlot()
    {
        return _UICard; 
    }

    public void SetCardInSlot(Card card, Transform fromTransform)
    {
        if(_moveTween!= null &&_moveTween.active) _moveTween.Kill(); 
        _UICard.transform.position = fromTransform.position;
        _cardInSlot = card;
        _UICard.AttachCard(card );
        _cardCanvasGroup.alpha = 1;
        _cardCanvasGroup.blocksRaycasts = true;
        _cardCanvasGroup.interactable = true;
        _UICard.MoveCardTo(this.transform);
        _UICard.FlipCard();

    }
    public void RemoveCardFromSlot(Transform toTransform)
    {
        _moveTween = _UICard.MoveCardTo(toTransform);
        _UICard.RemoveCard(_cardInSlot);
        _moveTween.OnComplete(() =>
        {
            _cardInSlot = null;
            _cardCanvasGroup.alpha = 0;
            _cardCanvasGroup.blocksRaycasts = false;
            _cardCanvasGroup.interactable = false;
        }); 

     
    }
}
