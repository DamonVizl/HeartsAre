using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHand : CardCollection
{
    public int startingHandSize = 5;
    public static event Action<int, Card> OnCardAddedToHand, OnCardRemovedFromHand;



    [SerializeField] private CardManager _cardManager; 

    //TrickScorer _trickScorer; 

/*    //a trick is a collection of cards used to score
    private Trick _trickAttempt;*/ //removed this in favour of selectedCards, that way we can do more with the selected cards (discard them, burn them to make super defenders, etc)

    //a list of currently selected cards
    List<Card> _selectedCards = new List<Card>();

    protected override void Start()
    {
        MaxCollectionSize = 7;
/*        _trickScorer = new TrickScorer();
*/        base.Start();
    }
    private void OnEnable()
    {
        //UI_Button_SubmitTrick.OnSubmitButtonPressed += SubmitTrick;
    }
    private void OnDisable()
    {
        //UI_Button_SubmitTrick.OnSubmitButtonPressed -= SubmitTrick;
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
                //play sfx
                SFXManager.Instance.PlayRandomSound(SFXName.CardDraw);
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
                Debug.Log("removing " + card.Value + card.Suit);
                CardDeselected(card);  ///remove it from the selected list
                OnCardRemovedFromHand?.Invoke(i, card);
                //_cardManager.DiscardCards(new List<Card> { card });
                //play sfx
                SFXManager.Instance.PlayRandomSound(SFXName.Discard); 
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
                //play sfx
                SFXManager.Instance.PlayRandomSound(SFXName.CardSelect);

            }
        }
        else
        {
            _selectedCards.Add(card);
            Debug.Log("number of selected cards is: " + _selectedCards.Count);

            SFXManager.Instance.PlayRandomSound(SFXName.CardSelect);
        }
    }
    public void CardDeselected(Card card)
    {
        _selectedCards.Remove(card);
        //_trickAttempt.RemoveCardFromTrick(card);
        SFXManager.Instance.PlayRandomSound(SFXName.CardDeselect);
    }
  /*  /// <summary>
    /// submites the _selectedCards to the trickscorer, if there is a score it submits an event that the currency manager can listen to
    /// </summary>
    public void SubmitTrick()
    {
        if (GameManager.Instance.TricksPlayedThisTurn >= GameManager.Instance.MaxNumberOfTricksPlayablePerTurn)
        {
            UI_MessageManager.Instance.ShowMessage("Max tricks played this turn"); 
            return; //if too many tricks have been played, return
        }
        int score = _trickScorer.CalculateHand(_selectedCards);
        //trick failed
        if (score == 0)
        {
            SFXManager.Instance.PlayFirstSound(SFXName.TrickFailed);
            return;
        }
        //trick succeeded
        else
        {
            SFXManager.Instance.PlayFirstSound(SFXName.TrickSucceeded);

*//*            List<Card> cardsToRemove = new List<Card>();

            //remove the cards
            foreach (Card card in _selectedCards)
            {
                cardsToRemove.Add(card);
            }
            Debug.Log("cards to remove" + cardsToRemove.Count);*//*
            _cardManager.DiscardCards(_selectedCards);
            _selectedCards.Clear();

            *//*            foreach (Card card in cardsToRemove)
                        {
                            RemoveCard(card);
                        }*/
            /*            //have the cardManager discard hte cards from the hand to the discard pile
                        _cardManager.DiscardCards(_selectedCards);*//*

            OnTrickScore?.Invoke(score);
            GameManager.Instance.TricksPlayedThisTurn++; 
            //once max tricks have been played, 
            if(GameManager.Instance.TricksPlayedThisTurn == GameManager.Instance.MaxNumberOfTricksPlayablePerTurn )
            {
                UI_MessageManager.Instance.ShowMessage("Max tricks played this turn, select end turn");

            }
        }


    }*/
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
