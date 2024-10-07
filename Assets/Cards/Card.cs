using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [field: SerializeField] public Suit Suit {get; private set;}
    [field: SerializeField] public int Value { get; private set; }

    [SerializeField] Image _baseCardImage; //reference to the UI Image component of the card.

    [SerializeField] AudioClip _cardSelectSound; //sound to be played when a card is selected

    // Shake the card transform for effect
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
    }

    public void SetCardValues(Suit cardSuit, int cardValue)
    {
        Suit = cardSuit;
        Value = cardValue;
        // add select card image here
    }
}


public enum Suit
{
    Hearts,
    Diamonds,
    Spades,
    Clubs
}
