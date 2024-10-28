using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHand : CardCollection
{
    public int startingHandSize = 5;
    public static event Action<int, Card> OnCardAddedToHand, OnCardRemovedFromHand;

    public static event Action<int> OnTrickScore;

    TrickScorer _trickScorer; 

/*    //a trick is a collection of cards used to score
    private Trick _trickAttempt;*/ //removed this in favour of selectedCards, that way we can do more with the selected cards (discard them, burn them to make super defenders, etc)

    //a list of currently selected cards
    List<Card> _selectedCards = new List<Card>();

    protected override void Start()
    {
        MaxCollectionSize = 7;
        _trickScorer = new TrickScorer();
        base.Start();
    }
    private void OnEnable()
    {
        UI_Button_SubmitTrick.OnSubmitButtonPressed += SubmitTrick;
    }
    private void OnDisable()
    {
        UI_Button_SubmitTrick.OnSubmitButtonPressed -= SubmitTrick;
    }

    public Card[] GetPlayerHand()
    {
        return _cards;
    }
    public override bool AddCard(Card card)
    {
        if (card == null) return false;
        //find an empty slot
        for (int i = 0; i < _cards.Length; i++)
        {
            if (_cards[i] == null)
            {
                _cards[i] = card;
                card.OnCardSelectedInHand += CardSelected;
                card.OnCardDeselectedInHand += CardDeselected;
                card.CurrentCardHolder = this;
                //update UI
                OnCardAddedToHand?.Invoke(i, card);
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// remove the card from the hand, if it's selected (which it should be, remove it from there too)
    /// </summary>
    /// <param name="card"></param>
    public void RemoveCard(Card card)
    {
        for (int i = 0; i < _cards.Length; i++)
        {
            if (_cards[i] == card)
            {
                //CardDeselected(card);  ///remove it from the selected list
                OnCardRemovedFromHand?.Invoke(i, card);
                _cards[i] = null;
            }
        }        
    }
    public void CardSelected(Card card)
    {
        if (_psm.GetCurrentState() == PlayState.ChooseSuperDefender)
        {
            if (_selectedCards.Count > 0)
                return;

            if (IsFaceCard(card))
            {
                _selectedCards.Add(card);
                Debug.Log("a face card has been selected to discard for a super defender");
            }
        }
        else
        {
            _selectedCards.Add(card);
        }
    }
    public void CardDeselected(Card card)
    {
        _selectedCards.Remove(card);
        //_trickAttempt.RemoveCardFromTrick(card);
    }
    /// <summary>
    /// submites the _selectedCards to the trickscorer, if there is a score it submits an event that the currency manager can listen to
    /// </summary>
    public void SubmitTrick()
    {
        int score = _trickScorer.CalculateHand(_selectedCards);
        if (score == 0) return;
        else
        {
            List<Card> cardsToRemove = new List<Card>();
            //remove the cards
            foreach (Card card in _selectedCards)
            {
                cardsToRemove.Add(card);
            }
            foreach(Card card in cardsToRemove)
            {
                RemoveCard(card);   
            }
            OnTrickScore?.Invoke(score);
        }


    }
    public List<Card> GetCurrentlySelectedCards()
    {
        return _selectedCards; 
    }

    public bool IsFaceCard(Card card)
    {
        int cardValue = card.GetCardValueAsInt();
        return cardValue == (int)Value.Jack || cardValue == (int)Value.Queen || cardValue == (int)Value.King;
    }

}
