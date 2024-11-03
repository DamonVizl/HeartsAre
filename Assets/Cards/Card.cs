using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

[Serializable]
public class Card
{
    #region Fields
    public CardCollection CurrentCardHolder; //a reference to where the card currently sits. 
    [field: SerializeField] public Suit Suit {get; private set;}
    [field: SerializeField] public Value Value { get; private set; }
    [SerializeField] AudioClip _cardSelectSound; //sound to be played when a card is selected

    //a sprite for the suit
    Sprite _suitSprite;
    public Sprite GetSuitSprite() { return _suitSprite; }
    //a sprite for the value
    Sprite _valueSprite;
    public Sprite GetValueSprite() { return _valueSprite; }
    //sprite for the image in the middle (e.g. number of hearts/picture of a queen)
    Sprite _centreImageSprite;
    public Sprite GetCentreSprite() { return _centreImageSprite; }
    #endregion
    #region Events
    public event Action<Color> OnColourChanged; //the card UI can sub to this when the card gets added to it (and unsub when it is removed)
    public event Action<Card> OnCardSelectedInHand, OnCardDeselectedInHand, OnCardSelectedInHeartDefenderHand, OnCardDeselectedInHeartDefenderHand;
    #endregion
    #region Constructor
    public Card(Suit suit, int value)
    {
        Suit = suit;
        Value = (Value) value;
        SetCardGraphics(Suit, Value);
    }
    #endregion

    /// <summary>
    /// sets up the card graphics based on the passed suit and value. Keeps cards flexible 
    /// </summary>
    /// <param name="suit"></param>
    /// <param name="value"></param>
    public void SetCardGraphics(Suit suit, Value value)
    {
        Debug.Log(suit.ToString() + " vs "+ Resources.Load<Sprite>(suit.ToString()));
        _suitSprite = Resources.Load<Sprite>(suit.ToString()); //suit sprites should be saved in the Resources folder named "heart.png" for e.g.
        _valueSprite = Resources.Load<Sprite>(value.ToString()); //value sprites (1,2,K,4, etc) should be saved in the Resources folder named "king.png" for e.g.
        _centreImageSprite = Resources.Load<Sprite>(value.ToString() + "_" + suit.ToString()); //card centre images should be saved in the Resources folder named "heart_king.png" for e.g. 
        // Set sprite colours based off suit. 
        SetSpriteColours(suit); 
    }

    private void SetSpriteColours(Suit suit)
    {
        Color color = Color.white;
        switch (suit)
        {
            case Suit.Hearts:
                color = new Color(255f, 0f, 0f); //red
                break;
            case Suit.Diamonds:
                color = new Color(255f, 85f, 0f); //orange
                break;
            case Suit.Clubs:
                color = new Color(30f, 30f, 30f); //almost black
                break;
            case Suit.Spades:
                color = new Color(10f, 110f, 0f); //deep green
                break;
        }

        //push event to update UI card with colour
        OnColourChanged?.Invoke(color);
    }


    public int GetCardValueAsInt()
    {
        return (int)Value;
    }

    public Suit GetCardSuit()
    {
        return Suit;
    }
    /// <summary>
    /// Select the card (when it's been clicked on), this is used when forming tricks, or updating heart cards
    /// </summary>
    public void SelectCard()
    {
        if(CurrentCardHolder is PlayerHand)
        {
            OnCardSelectedInHand?.Invoke(this);
        }
    }
    public void DeselectCard()
    {
        if (CurrentCardHolder is PlayerHand)
        {
            OnCardDeselectedInHand?.Invoke(this);
        }
    }
}


public enum Suit
{
    Clubs = 1,
    Diamonds = 2,
    Hearts = 3,
    Spades = 4,
}

[System.Flags]
public enum Value
{
    Ace = 1,
    Two = 2,
    Three = 3,
    Four  = 4,
    Five = 5,
    Six = 6,
    Seven = 7,
    Eight = 8,
    Nine = 9, 
    Ten = 10, 
    Jack = 11,
    Queen = 12,
    King = 13,
}