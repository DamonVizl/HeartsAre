using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//cards go into the discard pile when discarded from the hand, or after being played as tricks
public class DiscardPile : CardCollection
{
    protected override void Start()
    {
        MaxCollectionSize = 52; 
        base.Start();

    }
    public Card[] GetAllCards()
    {
        Debug.Log("discard pule size" + GetCurrentNumberOfCardsInCollection()); 
        return _cards;
    }
}
