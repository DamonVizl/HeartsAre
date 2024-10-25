using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class TrickScorer
{
    public static Action<String, int> OnTrickScored;
    private int CalculateScore(Value value, HandValues handValue)
    {
        int score = (int)handValue; 
        score *= (int)value;
        Debug.Log("Scored a " + value.ToString() + " high " + handValue.ToString() + " " + score); 
        OnTrickScored?.Invoke("Scored a " + value.ToString() + " high " + handValue.ToString(), score);
        return score; 
    }
    public int CalculateHand(List<Card> cards)
    {
        int score = 0;
        if (cards.Count <= 1) return score; //can't make a trick from 1 card
        if (IsRoyalFlush(cards))
        {
            switch(cards.Count)
            {
                case 5: return CalculateScore(GetHighestCardValue(cards), HandValues.FiveStraightRoyalFlush);
                case 6: return CalculateScore(GetHighestCardValue(cards), HandValues.SixStraightRoyalFlush);
                case 7: return CalculateScore(GetHighestCardValue(cards), HandValues.SevenStraightRoyalFlush);
            }
        }
        if (IsStraightFlush(cards))
        {
            switch (cards.Count)
            {
                case 5: return CalculateScore(GetHighestCardValue(cards), HandValues.FiveStraightFlush);
                case 6: return CalculateScore(GetHighestCardValue(cards), HandValues.SixStraightFlush);
                case 7: return CalculateScore(GetHighestCardValue(cards), HandValues.SevenStraightFlush);
            }
        }
        if (IsTwoTrips(cards))
        {
            return CalculateScore(GetHighestCardValue(cards), HandValues.TwoTrips);
        }
        if (IsQuads(cards))
        {
            return CalculateScore(GetHighestCardValue(cards), HandValues.Quads);
        }
        if (IsStraight(cards))
        {
            switch (cards.Count)
            {
                case 5: return CalculateScore(GetHighestCardValue(cards), HandValues.FiveStraight);
                case 6: return CalculateScore(GetHighestCardValue(cards), HandValues.SixStraight);
                case 7: return CalculateScore(GetHighestCardValue(cards), HandValues.SevenStraight);
            }
        }
        if (IsTrips(cards))
        {
            return CalculateScore(GetHighestCardValue(cards), HandValues.Trips);
        }
        if (IsThreePair(cards))
        {
            return CalculateScore(GetHighestCardValue(cards), HandValues.ThreePair);
        }
        if (IsTwoPair(cards))
        {
            return CalculateScore(GetHighestCardValue(cards), HandValues.TwoPair);
        }
        if (IsOnePair(cards))
        {
            return CalculateScore(GetHighestCardValue(cards), HandValues.OnePair);
        }
        else return 0;
    }
    private bool IsRoyalFlush(List<Card> cards)
    {
        // A Royal Flush is a special type of Straight Flush that ends with a King (king high)
        // It should consist of at least 5 cards: 9, 10, J, Q, K 
        return cards.Count >= 5 && IsStraightFlush(cards) && cards.Last().Value == Value.King;
    }
    private bool IsStraightFlush(List<Card> cards)
    {
        // A Straight Flush is when all cards are of the same suit AND they are in a consecutive sequence.
        return cards.Count >= 5 && IsFlush(cards) && IsStraight(cards);
    }
    private bool IsFlush(List<Card> cards)
    {
        // A Flush is when all cards are of the same suit.
        return cards.All(c => c.Suit == cards[0].Suit); //returns true if every card is of the same suit
    }
    private bool IsStraight(List<Card> cards)
    {
        // A Straight is when all cards have consecutive values, regardless of their suits.
        return cards.Count >=5 && cards.Select(c => (int)c.Value)
                    .Distinct() //distinct to make sure there aren't double ups
                    .OrderBy(value => value) //order them in lowest to heighest
                    .SequenceEqual(Enumerable.Range((int)cards[0].Value, cards.Count)); //compares the sequence of cards from cards[0] (the first card) to cards.count (the last card) against a sequence of numbers of the same range
    }
    private bool IsTwoTrips(List<Card> cards)
    {
        //group the cards by their value
        var groups = cards.GroupBy(c => c.Value).ToList();
        //return true if there is 1 group, and it's size is 2 (i.e. 3,3,3,7,7,7)
        return groups.Count == 2 && groups.All(g => g.Count() == 3);
    }
    private bool IsQuads(List<Card> cards)
    {
        //group the cards by their value
        var groups = cards.GroupBy(c => c.Value).ToList();
        //return true if there is 1 group, and it's size is 4 (i.e. 4,4,4,4)
        return groups.Count == 1 && groups.All(g => g.Count() == 4);
    }
    private bool IsTrips(List<Card> cards)
    {
        //group the cards by their value
        var groups = cards.GroupBy(c => c.Value).ToList();
        //return true if there is 1 group, and it's size is 3 (i.e. 10,10,10)
        return groups.Count == 1 && groups.All(g => g.Count() == 3);
    }
    private bool IsThreePair(List<Card> cards)
    {
        //group the cards by their value
        var groups = cards.GroupBy(c => c.Value).ToList();
        //return true if there is 1 group, and it's size is 2 (i.e. 2,2,3,3,6,6)
        return groups.Count == 3 && groups.All(g => g.Count() == 2);
    }
    private bool IsTwoPair(List<Card> cards)
    {
        //group the cards by their value
        var groups = cards.GroupBy(c => c.Value).ToList();
        //return true if there is 1 group, and it's size is 2 (i.e. 4,4,6,6)
        return groups.Count == 2 && groups.All(g => g.Count() == 2);
    }

    private bool IsOnePair(List<Card> cards)
    {
        //group the cards by their value
        var groups = cards.GroupBy(c => c.Value).ToList();
        //return true if there is 1 group, and it's size is 2 (i.e. 2,2)
        return groups.Count == 1 && groups.Any(g => g.Count() == 2);
    }
    //gets the highest value of the hand

    private Value GetHighestCardValue(List<Card> cards)
    {
        return cards.Max(c => c.Value);
    }
}

public enum HandValues
{
    OnePair = 1, 
    TwoPair = 2,
    ThreePair = 3,
    Trips = 4,
    FiveStraight = 5,
    Quads = 6,
    SixStraight = 7,
    TwoTrips = 8,
    SevenStraight = 9,
    FiveStraightFlush =10,
    FiveStraightRoyalFlush = 11,
    SixStraightFlush = 12,
    SevenStraightFlush = 13,
    SixStraightRoyalFlush = 14,
    SevenStraightRoyalFlush = 15,
}
