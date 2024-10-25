using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class UI_PlayerHand : MonoBehaviour
{
    private UI_CardSlot[] _cardSlots;

    private void OnEnable()
    {
        _cardSlots = GetComponentsInChildren<UI_CardSlot>(); //fill the array with the children slots
        PlayerHand.OnCardAddedToHand += UpdateCardAtSlot;
        PlayerHand.OnCardRemovedFromHand += RemoveCardAtSlot;
    }
    private void OnDisable()
    {
        PlayerHand.OnCardAddedToHand -= UpdateCardAtSlot;
        PlayerHand.OnCardRemovedFromHand -= RemoveCardAtSlot;

    }

    private void UpdateCardAtSlot(int index, Card card)
    {
        if(index < _cardSlots.Length)
        {
            _cardSlots[index].SetCardInSlot(card);
        }
    }
    private void RemoveCardAtSlot(int index, Card card)
    {
        if(index < _cardSlots.Length)
        {
            _cardSlots[index].RemoveCardFromSlot(); 
        }
    }
}
