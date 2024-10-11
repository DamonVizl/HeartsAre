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
    PlayStateMachine _psm; 

    private void OnEnable()
    {
        UI_DrawDeck.OnDrawDeckClicked += DrawCardFromDeckToHand;
        _psm = GameObject.FindFirstObjectByType<PlayStateMachine>(); //TODO: hacky, fix later. 
    }
    private void OnDisable()
    {
        UI_DrawDeck.OnDrawDeckClicked -= DrawCardFromDeckToHand;
    }

    /// <summary>
    /// takes a card from the draw pile and puts it in the hand
    /// </summary>
    private void DrawCardFromDeckToHand()
    {
        //only draw a card if in the player turn (and haven't exceed max allowable cards in hand)
        if(_psm.GetCurrentState() == PlayState.PlayerTurn && _hand.GetCurrentNumberOfCardsInCollection()<_hand.MaxCollectionSize)
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
    /// select a card from the player hand, can then do different things with it like place it in a meld, move it to a heart Defender position, etc
    /// </summary>
    public void SelectPlayerHandCard()
    {

    }

}
