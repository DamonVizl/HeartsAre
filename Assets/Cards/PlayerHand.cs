using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : CardCollection
{
    public int startingHandSize = 5;
    public static event Action<int, Card> OnCardAddedToHand;

    public event Action<int> OnTrickScore;

    //a trick is a collection of cards used to score
    private Trick _trickAttempt;

    protected override void Start()
    {
        MaxCollectionSize = 7;
        _trickAttempt = new Trick();
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
    public void CardSelected(Card card)
    {
        _trickAttempt.AddCardToTrick(card);
    }
    public void CardDeselected(Card card)
    {
        _trickAttempt.RemoveCardFromTrick(card);
    }
    public void SubmitTrick()
    {
        Debug.Log("Submitting trick for scoring");
        int score = _trickAttempt.SubmitTrick();
        if (score == 0) return;
        else OnTrickScore?.Invoke(score);

    }
}
