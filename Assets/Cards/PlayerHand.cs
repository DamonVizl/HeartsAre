using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    private Deck deck;
    public List<Card> playerHandCards = new List<Card>();

    private const int MaxHandSize = 5;

    private void Start()
    {
        SetStartingParameters();
    }

    public void DrawStartingHand() // draws the starting hand
    {
        for (int i = 0; i < MaxHandSize; i++)
        { 
            deck.DrawCard();
        }
    }

    public void AddCardToHand(Card card) // adds a card to the player's hand
    {
        playerHandCards.Add(card);
    }

    void SetStartingParameters()
    {
        deck = FindObjectOfType<Deck>();
    }

    public List<Card> GetPlayerHand()
    {
        return playerHandCards;
    }


}
