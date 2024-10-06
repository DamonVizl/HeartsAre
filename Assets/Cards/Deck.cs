using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] private List<Card> cards = new List<Card>();
    public GameObject cardPrefab;
    private GameObject cardParent;

    public void Start()
    {
        GenerateDeck();
        ShuffleDeck();
    }

    // generates all standard cards in the deck
    public void AddStandardCards(Suite suite)
    {
        for (int i = 1; i <= 13; i++)
        {
            GameObject playingCard = Instantiate(cardPrefab, cardParent.transform);

            Card card = playingCard.GetComponent<Card>();

            card.SetCardValues(suite, i);
            cards.Add(card);
        }
    }

    public void GenerateDeck()
    {
        // generate a parent object for cards to organize under in the hierarchy
        GenerateParentObject();
        // generate all standard cards in the deck
        foreach (Suite suite in (Suite[])System.Enum.GetValues(typeof(Suite)))
        {
            AddStandardCards(suite);
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



    // generates a parent object for card objects
    void GenerateParentObject()
    {
        cardParent = new GameObject("Cards Parent Object");
    }
}
