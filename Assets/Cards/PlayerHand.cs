using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : CardCollection
{
    public int startingHandSize = 5;
    public static event Action<int,Card> OnCardAddedToHand;

    protected override void Start()
    {
        MaxCollectionSize = 7; 
        base.Start();
    }

    public Card[] GetPlayerHand()
    {
        return _cards;
    }
    public override void AddCard(Card card)
    {
        if (card == null) return;
        //find an empty slot
        for(int i = 0; i < _cards.Length; i++)
        {
            if (_cards[i] == null)
            {
                Debug.Log("Card "+card.Value+" added to "+ i);
                _cards[i] = card;
                //update UI
                OnCardAddedToHand?.Invoke(i, card);
                return;
            }
        }
    }
/*    public override Card DrawCard(int index)
    {
        OnCardAddedToHand?.Invoke(index, null); 
        return base.DrawCard();

    }*/
}
