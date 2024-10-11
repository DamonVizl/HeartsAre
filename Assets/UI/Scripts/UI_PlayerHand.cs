using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PlayerHand : MonoBehaviour
{
    private UI_CardSlot[] _cardSlots;

    private void OnEnable()
    {
        _cardSlots = GetComponentsInChildren<UI_CardSlot>(); //fill the array with the children slots
        PlayerHand.OnCardAddedToHand += UpdateCardAtSlot;
    }
    private void OnDisable()
    {
        PlayerHand.OnCardAddedToHand -= UpdateCardAtSlot;

    }

    private void UpdateCardAtSlot(int index, Card card)
    {
        if(index < _cardSlots.Length)
        {
            _cardSlots[index].SetCardInSlot(card);
        }
    }


}
