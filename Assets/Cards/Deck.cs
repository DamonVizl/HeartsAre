using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] private List<Card> cards = new List<Card>();
    public GameObject cardPrefab;
    private GameObject cardParent;

    public PlayerHand playerHand;

    public void Start()
    {
        SetStartingParameters();
        GenerateDeck();
        ShuffleDeck();
        playerHand.DrawStartingHand();
    }

    // generates all standard cards in the deck
    public void AddStandardCards(Suit suit)
    {
        for (int i = 1; i <= 13; i++)
        {
            GameObject playingCard = Instantiate(cardPrefab, cardParent.transform);

            Card card = playingCard.GetComponent<Card>();

            card.SetCardValues(suit, i);
            cards.Add(card);
        }
    }

    public void GenerateDeck()
    {
        // generate a parent object for cards to organize under in the hierarchy
        GenerateParentObject();
        // generate all standard cards in the deck
        foreach (Suit suit in (Suit[])System.Enum.GetValues(typeof(Suit)))
        {
            AddStandardCards(suit);
        }
    }

    public void ShuffleDeck()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            int randomCard = Random.Range(0, cards.Count);
            Card temp = cards[i];
            cards[i] = cards[randomCard];
            cards[randomCard] = temp;
        }
    }

    public void DrawCard()
    {
        if (cards.Count == 0)
            return;

        Card drawnCard = cards[0];
        cards.RemoveAt(0);
        playerHand.AddCardToHand(drawnCard);
        
    }



    // generates a parent object for card objects
    void GenerateParentObject()
    {
        cardParent = new GameObject("Cards Parent Object");
    }

    void SetStartingParameters()
    {
        playerHand = FindObjectOfType<PlayerHand>();
    }
}
