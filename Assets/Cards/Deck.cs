using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Deck : CardCollection
{
    #region Setup Methods
    protected override void Start()
    {
        MaxCollectionSize = 52;
        //InitializeSpriteMap(); // create a spritemap using a dictionary to assign sprite art based on card's value
        //SetStartingParameters();
        base.Start();
        GenerateDeck();
        ShuffleCards();
    }

    /// <summary>
    /// Generates a deck of 52 standard playing cards
    /// </summary>
    public void GenerateDeck()
    {
        int i = 0; //counter for whole deck. 

        // generate all standard cards in the deck
        foreach (Suit suit in (Suit[])System.Enum.GetValues(typeof(Suit)))
        {
            for(int j = 1; j< 14; j++) //counts from 1 to 13
            {
                Card card = new Card(suit, j); //the card sets it's self up. check Card.cs constructor for details
                AddCard(card);
                //_cards[i] = card;
                i++;
            }
        }
    }

    #endregion
}
