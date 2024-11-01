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
        // Check if there is at least one card in hand before allowing discard
        if (_hand.GetPlayerHand().Length > 0)
        {
            // if selects the trash bin while in ChooseSuperDefender state
            if (_psm.GetCurrentState() == PlayState.ChooseSuperDefender)
            {
                List<Card> cardsToRemove = new List<Card>(_hand.GetCurrentlySelectedCards());
                if (cardsToRemove.Count > 0)
                {
                    Card cardForSuperDefender = cardsToRemove[0]; // get the card selected for the super defender sacrifice
                    DiscardCards(cardsToRemove);

                    if (_superDefenderManager?.GetSelectedDefender() != null)
                    {
                        _superDefenderManager.GetSelectedDefender().ChangeToSuperDefender(cardForSuperDefender);
                        _psm.TransitionToPreviousState();
                    }
                }
            }
            else if(GameManager.Instance.HandRefillsThisTurn >= GameManager.Instance.MaxHandRefillsPerTurn) {
                UI_MessageManager.Instance.ShowMessage("Max draws exceeded"); 
                return;  
            }
            {
                List<Card> cardsToRemove = new List<Card>(_hand.GetCurrentlySelectedCards());
                if (cardsToRemove.Count > 0)
                {
                    DiscardCards(cardsToRemove);
                    //_psm.TransitionToState(PlayState.HeartDefenders);
                }
            }
        }
        else
        {
            Debug.Log("No cards in hand to discard.");
        }
    }

    void DiscardCards(List<Card> cardsToRemove)
    {
        foreach (Card card in cardsToRemove)
        {
            _discardPile.AddCard(card);
            OnCardDiscardedFromHand?.Invoke(card);
        }
        foreach (Card card in new List<Card>(cardsToRemove)) // Iterate over a copy to avoid modifying during removal
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
            Debug.Log("draw pile empty");
            ReshuffleDiscardPileIntoDrawPile();
        }
        //only draw a card if in the player turn 
        if (_psm.GetCurrentState() == PlayState.PlayerTurn)
        {
            if (GameManager.Instance.HandRefillsThisTurn >= GameManager.Instance.MaxHandRefillsPerTurn)
            {
                //if too many hands have been refilled, return
                UI_MessageManager.Instance.ShowMessage("Hand refills have exceeded maximum"); 
                return; 
            }
            if (_hand.GetCurrentNumberOfCardsInCollection() >= _hand.MaxCollectionSize)
            {
                //if the hand is full, don't draw
                UI_MessageManager.Instance.ShowMessage("Hand full, cannot draw"); 
                return;  
            }
                         //fill the hand with cards
                while (_hand.GetCurrentNumberOfCardsInCollection() < _hand.MaxCollectionSize)
            {
                Debug.Log("Drawing a card"); 
                Card drawnCard = _drawPile.DrawCard();
                if (!_hand.AddCard(drawnCard))
                {
                    //if the card couldn't be added, return it back to the original. TODO: I don't think this is putting it back to original spot, it's putting it into the first empty (which may be...)
                    _drawPile.AddCard(drawnCard);
                }
            }
            //increase the number of draws
            GameManager.Instance.HandRefillsThisTurn++; 
            
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
            Debug.Log($"adding {card} to draw pile"); 
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
