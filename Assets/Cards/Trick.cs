using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// a Trick is a group of scoring cards (not a card collection, I dont want to AddCard here, it's not necessary)
/// </summary>
public class Trick 
{
    List<Card> _cardsAddedToTrick; 
    TrickScorer _trickScorer;
     
    public Trick()
    {
        _trickScorer = new TrickScorer();
        _cardsAddedToTrick = new List<Card>();
    }
    public void AddCardToTrick(Card card)
    {
        _cardsAddedToTrick.Add(card);
    }
    public void RemoveCardFromTrick(Card card)
    {
        _cardsAddedToTrick.Remove(card);    
    }
    //submit the trick and see if it's a valid scoring hand. returns the score
    public int SubmitTrick()
    {
        int trickScore = _trickScorer.GetScore(_cardsAddedToTrick); 
        return trickScore;
    }

    private List<Card> SortCards(List<Card> cards)
    {
        //sort the cards lowest to highest value, Clubs,Diamonds,Hearts, Spades
        cards.Sort((x, y) =>
        {
            int valueComparison = x.Value.CompareTo(y.Value);
            //if the values are the same, i.e. 2 of clubs and 2 of diamonds, sort by suit
            if (valueComparison == 0)
            {
                // If values are the same, compare by suit.
                return x.Suit.CompareTo(y.Suit);
            }
            return valueComparison;
        });
        return cards;
    }
}
