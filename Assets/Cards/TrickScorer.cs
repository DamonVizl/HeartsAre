using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TrickScorer : MonoBehaviour
{
    public static Action<String,int> OnTrickScored; 
    public int GetScore(List<Card> cards)
    {
        int score = 0; 
        if(cards.Count <= 1) return score; //can't make a trick from 1 card
        

        if (IsTwoPair(cards)) 
        {
            score = (int)HandValues.TwoPair;
            OnTrickScored?.Invoke("Scored a Two Pair!", score); 
            return score;
        }
        if (IsOnePair(cards))
        {
            score = (int)HandValues.OnePair;
            OnTrickScored?.Invoke("Scored a Pair!", score);
            return score;
        }
        else return 0;
    }
    private bool IsTwoPair(List<Card> cards)
    {
        //group the cards by their value
        var groups = cards.GroupBy(c => c.Value).ToList();
        //return true if there is 1 group, and it's size is 2 (i.e. 1 pair)
        return groups.Count == 2 && groups.All(g => g.Count() == 2);
    }

    private bool IsOnePair(List<Card> cards)
    {
        //group the cards by their value
        var groups = cards.GroupBy(c => c.Value).ToList();
        //return true if there is 1 group, and it's size is 2 (i.e. 1 pair)
        return groups.Count == 1 && groups.Any(g => g.Count() == 2);
    }
}

public enum HandValues
{
    OnePair = 1, 
    TwoPair = 2,
    ThreePair = 3,
    Trips = 4,
    Straight = 5,
    Quads = 6,
    TwoTrips = 7,
    Flush = 8,
    StraightFlush = 9,
    RoyalFlush = 10,
}
