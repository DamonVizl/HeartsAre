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
    protected Card[] _cards;
    public int MaxCollectionSize { get; protected set; }
    #endregion

    #region Init
    protected virtual void Start()
    {
        //set the card array to it's max size
        _cards = new Card[MaxCollectionSize];
    }
    #endregion
    #region Deck Manipulation Methods

    /// <summary>
    /// Shuffles the order of the deck
    /// </summary>
    public void ShuffleCards()
    {
        for (int i = 0; i < _cards.Length; i++)
        {
            int randomCard = UnityEngine.Random.Range(0, _cards.Length);
            Card temp = _cards[i];
            _cards[i] = _cards[randomCard];
            _cards[randomCard] = temp;
        }
    }

    /// <summary>
    /// Draw the top card on the deck and return it. remove the card drawn from this Deck
    /// </summary>
    public virtual Card DrawCard()
    {
        Card card = null;
        if (_cards.Length == 0)
            return card;

        //loop through and find the first non-null card
        for(int i = 0; i<_cards.Length; i++)
        {
            if(_cards[i] == null) continue;
            
            card = _cards[i]; //store the card
            _cards[i] = null; //remove the card
            break;
        }
        return card;

    }
    /// <summary>
    /// draws each card from this cardCollection (probably only used by discard pile
    /// </summary>
    /// <returns></returns>
    public virtual List<Card> TakeAllCards()
    {
        List<Card> cards = new List<Card>();
        cards.Add(DrawCard());
        return cards; 
    }
    //base AddCard method, adds the card to the list. extend this in the children classes to update UI in a bespoke way etc
    public virtual bool AddCard(Card card)
    {
        if(card != null)
        {
            //find first empty spot
            for(int i = 0; i<_cards.Length;i++)
            {
                if (_cards[i] == null)
                {
                    _cards[i] = card;
                    //sets this as the card collection that the card is in (i.e. if it's just been added to the hand from the deck)
                    card.CurrentCardHolder = this;
                    return true;
                }
            }
        }
        return false;
    }

    public int GetCurrentNumberOfCardsInCollection()
    {
        int count = 0; 
        for(int i = 0;i<_cards.Length; i++)
        {
            if (_cards[i] != null) count++;
        }
        //Debug.Log("getting cards from " + this.name + " there are " + _cards.Count + " cards");
        return count;
    }
    #endregion
}
