using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class UI_Card : MonoBehaviour
{
    [SerializeField] Image _backOfCard; 
    [SerializeField] Image _backgroundCardImage; 
    [SerializeField] Image _suitImage; 
    [SerializeField] Image _valueImage;
    [SerializeField] Image _centreImage;

    public void AttachCard(Card card)
    {
        UpdateCardSprites(card);
        card.OnColourChanged += UpdateCardGraphics;
    }
    public void RemoveCard(Card card)
    {
        card.OnColourChanged -= UpdateCardGraphics;
        CleanCard();
    }
    private void UpdateCardGraphics(Color colour)
    {
        _backgroundCardImage.enabled = false;
        _valueImage.color = colour;
        _suitImage.color = colour;
        _centreImage.color = colour;
        _backgroundCardImage.enabled = true;
    }
    private void UpdateCardSprites(Card card)
    {
        _valueImage.sprite = card.GetValueSprite();
        _suitImage.sprite = card.GetSuitSprite();
        _centreImage.sprite = card.GetCentreSprite();
    }
    private void CleanCard()
    {
        _valueImage.sprite = null;
        _suitImage.sprite = null;
        _centreImage.sprite = null;
        _backgroundCardImage.enabled = false;
    }
}
