using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : CardCollection
{
    [SerializeField] 
    public GameObject cardPrefab;
    private GameObject cardParent;

    //public PlayerHand playerHand; //LLB. Removed this so the deck doesn't know about the hand. 

    [SerializeField] List<Sprite> cardSprites;

    private Dictionary<string, Sprite> cardSpriteMap = new Dictionary<string, Sprite>(); // Dictionary with all the card sprites

    public UI_Manager ui_Manager;

    #region Setup Methods
    public void Start()
    {
        InitializeSpriteMap(); // create a spritemap using a dictionary to assign sprite art based on card's value
        //SetStartingParameters();
        GenerateDeck();
        ShuffleCards();
    }
    // generates a parent object for card objects (to keep the hierarchy tidy)
    void GenerateParentObject()
    {
        cardParent = new GameObject("Cards Parent Object");
    }

    // creates a sprite map in order to assign card sprites based on their value
    void InitializeSpriteMap()
    {
        foreach (Sprite sprite in cardSprites)
        {
            string spriteName = sprite.name;
            cardSpriteMap[spriteName] = sprite;
        }
    }

    /// <summary>
    /// Generates a deck of 52 standard playing cards
    /// </summary>
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


    // generates all standard cards in the deck (for the passed suit)
    public void AddStandardCards(Suit suit)
    {
        for (int i = 1; i <= 13; i++)
        {
            GameObject playingCard = Instantiate(cardPrefab, cardParent.transform);

            Card card = playingCard.GetComponent<Card>();

            string key = "card_" + suit.ToString().ToLower() + "_" + i.ToString(); // create a key that gets a string for the card's sprite based on the card's value

            Sprite cardSprite = cardSpriteMap[key]; // assign card art based on the key

            card.SetCardValues(suit, i, cardSprite);
            _cards.Add(card);
        }
    }
    #endregion
    #region Removed Stuff

    //LLB. Removed these two methods as the player should draw the cards, not the deck feeding them to the player, this way the deck doesn't know about the player. 
    /*    void SetStartingParameters()
        {
            playerHand = FindObjectOfType<PlayerHand>();
            ui_Manager = FindObjectOfType<UI_Manager>();
        }*/

    /*    void DealHand()
        {
            //playerHand.DrawStartingHand();
            cardParent.SetActive(false); // disable physical gameObjects from UI as temp solution
        }*/
    #endregion
}
