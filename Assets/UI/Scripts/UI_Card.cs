using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 
 

public class UI_Card : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image _backOfCard; 
    [SerializeField] Image _backgroundCardImage; 
    [SerializeField] Image _suitImage; 
    [SerializeField] Image _valueImage;
    [SerializeField] Image _centreImage;

    //Reference to the tween 
    Tween _tween; 

    Card _cardReference;
    bool _cardSelected = false; //true when the card has been clicked on, false if not. used for creating tricks

    //has the card been flipped (true for face up, false for face down)
    bool _cardFlipped = false;

    public void AttachCard(Card card)
    {
        UpdateCardSprites(card);
        card.OnColourChanged += UpdateCardGraphics;
        _cardReference = card;
    }
    public void RemoveCard(Card card)
    {
        card.OnColourChanged -= UpdateCardGraphics;
        CleanCard();
        _cardReference = null;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_cardSelected) SelectCard();
        else UnSelectCard(); 
        

    }
    private void SelectCard()
    {
        //if there is a tween in motion, do nothing and return
        if (_tween.IsActive()) return ; 
        //Do card selected stuff on the card
        _cardReference.SelectCard();
        //move card up
        _tween = transform.DOMoveY(transform.position.y + 20f, 0.2f);
        _cardSelected = true; 
    }
    private void UnSelectCard()
    {
        //if the card is currently tweening, return without doing anything
        if (_tween.IsActive()) return;
        //deselect the card, move it down and set _cardSelected to false
        _cardReference.DeselectCard(); 
        _tween = transform.DOMoveY(transform.position.y - 20f, 0.2f);
        _cardSelected = false;
    }

    public void FlipCard()
    {
        //first half of flip
        transform.DORotate(new Vector3(0, 90, 0), 0.3f).SetEase(Ease.OutQuad).OnComplete(() =>
        {

            _backOfCard.enabled = _cardFlipped; 
/*            if (_cardFlipped) _backOfCard.enabled = true;
            else _backOfCard.enabled = false;*/
       
            //second half of flip
            transform.DORotate(new Vector3(0, 0, 0), 0.3f).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                //reset the rotation
                //transform.rotation = Quaternion.Euler(0, 0, 0);
                _cardFlipped = !_cardFlipped;
            });
        });
    }

    #region Graphics Stuff
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


    #endregion
}
