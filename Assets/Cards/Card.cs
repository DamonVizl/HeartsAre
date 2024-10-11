using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Card
{
    [field: SerializeField] public Suit Suit {get; private set;}
    [field: SerializeField] public int Value { get; private set; }

    //[SerializeField] Image _baseCardImage; //reference to the UI Image component of the card.

    [SerializeField] AudioClip _cardSelectSound; //sound to be played when a card is selected

    //[SerializeField] Sprite _cardSprite; // used to store the assigned sprite of the card


    //a sprite for the suit
    Sprite _suitSprite;
    public Sprite GetSuitSprite() { return _suitSprite; }
    //a sprite for the value
    Sprite _valueSprite;
    public Sprite GetValueSprite() { return _valueSprite; }
    //sprite for the image in the middle (e.g. number of hearts/picture of a queen)
    Sprite _centreImageSprite;
    public Sprite GetCentreSprite() { return _centreImageSprite; }
    #region Events
    public event Action<Color> OnColourChanged; //the card UI can sub to this when the card gets added to it (and unsub when it is removed)
    #endregion
    #region Constructor
    public Card(Suit suit, int value)
    {
        Suit = suit;
        Value = value;
        SetCardGraphics(Suit, Value);
    }
    #endregion

    /// <summary>
    /// sets up the card graphics based on the passed suit and value. Keeps cards flexible 
    /// </summary>
    /// <param name="suit"></param>
    /// <param name="value"></param>
    public void SetCardGraphics(Suit suit, int value)
    {
        Debug.Log(suit.ToString() + " vs "+ Resources.Load<Sprite>(suit.ToString()));
        _suitSprite = Resources.Load<Sprite>(suit.ToString()); //suit sprites should be saved in the Resources folder named "heart.png" for e.g.
        _valueSprite = Resources.Load<Sprite>(value.ToString()); //value sprites (1,2,K,4, etc) should be saved in the Resources folder named "king.png" for e.g.
        _centreImageSprite = Resources.Load<Sprite>("Big"+suit.ToString()); //card centre images should be saved in the Resources folder named "heart_king.png" for e.g. 
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

    /*    // Shake the card transform for effect
        public void ShakeCard(float amount)
        {
            //do something to the _baseCardImage
        }
        //on card selected, highlight in UI somehow. Potentially set the card as selected in a higher HandManager/DrawManager/DiscardManager etc. 
        public void SelectCard()
        {
            //move card up a little
            _baseCardImage.transform.position = new Vector3(_baseCardImage.transform.position.x, _baseCardImage.transform.position.y + 50, _baseCardImage.transform.position.z);
            //shake card a little

            //play a sound
            SFXManager.Instance.PlaySound(_cardSelectSound);
        }
        public void DeselectCard()
        {
            //move card back down to original spot
            _baseCardImage.transform.position = new Vector3(_baseCardImage.transform.position.x, _baseCardImage.transform.position.y - 50, _baseCardImage.transform.position.z);
            //
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            SelectCard(); 
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            DeselectCard(); 
        }*/

/*    public void SetCardValues(Suit cardSuit, int cardValue, Sprite cardImage)
    {
        this.Suit = cardSuit;
        this.Value = cardValue;
        _cardSprite = cardImage;
    }*/

/*    public Sprite GetCardSprite()
    {
        return _cardSprite;
    }*/

    public int GetCardValue()
    {
        return Value;
    }

    public Suit GetCardSuit()
    {
        return Suit;
    }
}


public enum Suit
{
    Hearts,
    Diamonds,
    Spades,
    Clubs
}
