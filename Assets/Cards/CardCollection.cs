using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base collection of cards that Decks, Hands, etc inherit from
/// </summary>
public class CardCollection : MonoBehaviour
{
    #region Fields
    protected List<Card> _cards = new List<Card>();
    public int MaxCollectionSize { get; protected set; }
    #endregion

    #region Deck Manipulation Methods

    /// <summary>
    /// Shuffles the order of the deck
    /// </summary>
    public void ShuffleCards()
    {
        for (int i = 0; i < _cards.Count; i++)
        {
            int randomCard = UnityEngine.Random.Range(0, _cards.Count);
            Card temp = _cards[i];
            _cards[i] = _cards[randomCard];
            _cards[randomCard] = temp;
        }
    }

    /// <summary>
    /// Draw the top card on the deck and return it. remove the card drawn from this Deck
    /// </summary>
    public Card DrawCard()
    {
        if (_cards.Count == 0)
            return null;

        Card drawnCard = _cards[0];
        _cards.RemoveAt(0);
        //playerHand.AddCardToHand(drawnCard);
        return drawnCard;

    }
    //base AddCard method, adds the card to the list. extend this in the children classes to update UI in a bespoke way etc
    public virtual void AddCard(Card card)
    {
        if(card != null)
        {
            _cards.Add(card);
        }
    }

    public int GetNumCardsInCollection()
    {
        return _cards.Count;
    }
    #endregion
}
