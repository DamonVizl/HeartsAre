using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : CardCollection
{
    public int startingHandSize = 5;
    public static event Action<Card> OnCardAddedToHand; 

    private void Start()
    {
        MaxCollectionSize = 7; 
    }

    public List<Card> GetPlayerHand()
    {
        return _cards;
    }
    public override void AddCard(Card card)
    {
        base.AddCard(card);
        //update UI
        OnCardAddedToHand?.Invoke(card);
    }
}
