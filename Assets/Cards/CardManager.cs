using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// intermediary between decks/hands/etc. pushes UI events as well
/// </summary>
public class CardManager : MonoBehaviour
{
    [SerializeField] Deck _drawPile;
    [SerializeField] PlayerHand _hand;
    [SerializeField] List<Meld> _melds;
    [SerializeField] DiscardPile _discardPile; 
    PlayStateMachine _psm;
    SuperDefenderManager _superDefenderManager;


    #region Events
    public static event Action<Card> OnCardAddedToHandFromDrawPile, OnCardDiscardedFromHand; 
    #endregion
    private void OnEnable()
    {
        UI_DrawPile.OnDrawDeckClicked += DrawCardFromDeckToHand;
        UI_DiscardCardsButton.OnDiscardCardButtonPressed += DiscardCardsFromHand;

        _psm = GameObject.FindFirstObjectByType<PlayStateMachine>(); //TODO: hacky, fix later. 
        _superDefenderManager = FindObjectOfType<SuperDefenderManager>();
    }
    private void OnDisable()
    {
        UI_DrawPile.OnDrawDeckClicked -= DrawCardFromDeckToHand;
        UI_DiscardCardsButton.OnDiscardCardButtonPressed -= DiscardCardsFromHand;

    }

    /// <summary>
    /// takes the selected cards from the player hand and discards them
    /// </summary>
    private void DiscardCardsFromHand()
    {
        List<Card> cardsToRemove = new List<Card>();
        // if selects the trash bin while in ChooseSuperDefender state
        if (_psm.GetCurrentState() == PlayState.ChooseSuperDefender)
        {
            Card _cardSelectedForSuperDefender = _hand.GetCurrentlySelectedCards()[0]; // get the card selected for the super defender sacrifice
            DiscardCards(cardsToRemove);
            // get the selected HeartDefender and call the ChangeSuperDefender() method with the selected face card
            if (_superDefenderManager != null && _superDefenderManager.GetSelectedDefender() != null && _cardSelectedForSuperDefender != null)
            {
                _superDefenderManager.GetSelectedDefender().ChangeToSuperDefender(_cardSelectedForSuperDefender);
                _psm.TransitionToPreviousState();
            }
        }
        else if (_psm.GetCurrentState() == PlayState.DiscardCards)
        {
            DiscardCards(cardsToRemove);
            //transition to the Heart Defenders state to prepare for attack, ending the discard phase
            _psm.TransitionToState(PlayState.HeartDefenders);
        }
    }

    void DiscardCards(List<Card> cardsToRemove)
    {
        foreach (Card card in _hand.GetCurrentlySelectedCards())
        {
            _discardPile.AddCard(card);
            cardsToRemove.Add(card);
            OnCardDiscardedFromHand?.Invoke(card);
        }
        //seperate for loop so im not modifying the list while enumerating through it
        foreach (Card card in cardsToRemove)
        {
            _hand.RemoveCard(card);
        }
    }
    /// <summary>
    /// takes a card from the draw pile and puts it in the hand
    /// </summary>
    private void DrawCardFromDeckToHand()
    {
        if (_drawPile.GetCurrentNumberOfCardsInCollection() == 0)
        {
            ReshuffleDiscardPileIntoDrawPile();
        }
        //only draw a card if in the player turn (and haven't exceed max allowable cards in hand)
        if (_psm.GetCurrentState() == PlayState.PlayerTurn && _hand.GetCurrentNumberOfCardsInCollection() < _hand.MaxCollectionSize)
        {
            //Debug.Log("Drawing a card"); 
            Card drawnCard = _drawPile.DrawCard();
            if (!_hand.AddCard(drawnCard))
            {
                //if the card couldn't be added, return it back to the original. TODO: I don't think this is putting it back to original spot, it's putting it into the first empty (which may be...)
                _drawPile.AddCard(drawnCard);
            }
        }
    }
    /// <summary>
    /// takes all the discard pile cards and adds them to the draw pile. Then reshuffles. 
    /// </summary>
    private void ReshuffleDiscardPileIntoDrawPile()
    {
        //take all the cards from the discard pile
        List<Card> takenDiscardCards = _discardPile.TakeAllCards(); 
        
        foreach (Card card in takenDiscardCards)
        {
            _drawPile.AddCard(card);
        }

        //shuffle cards.
        _drawPile.ShuffleCards(); 
    }

    /// <summary>
    /// select a card from the player hand, can then do different things with it like place it in a meld, move it to a heart Defender position, etc
    /// </summary>
    public void SelectPlayerHandCard()
    {

    }

}
