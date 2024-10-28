using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class UI_PlayerHand : MonoBehaviour
{
    private UI_CardSlot[] _cardSlots;
    [SerializeField] Transform _drawPileTransform;
    [SerializeField] Transform _discardPileTransform;
    public GameObject _submitTrickBtn;
    public GameObject _endTurnBtn;

    private void OnEnable()
    {
        _cardSlots = GetComponentsInChildren<UI_CardSlot>(); //fill the array with the children slots
        PlayerHand.OnCardAddedToHand += AddCardAtSlot;
        PlayerHand.OnCardRemovedFromHand += RemoveCardAtSlot;
    }
    private void OnDisable()
    {
        PlayerHand.OnCardAddedToHand -= AddCardAtSlot;
        PlayerHand.OnCardRemovedFromHand -= RemoveCardAtSlot;

    }

    private void AddCardAtSlot(int index, Card card)
    {
        if(index < _cardSlots.Length)
        {
            _cardSlots[index].SetCardInSlot(card, _drawPileTransform);
        }
    }
    private void RemoveCardAtSlot(int index, Card card)
    {
        if(index < _cardSlots.Length)
        {
            _cardSlots[index].RemoveCardFromSlot(_discardPileTransform); 
        }
    }

    public void DisablePlayerHandInteractionBtns()
    {
        _submitTrickBtn.SetActive(false);
        _endTurnBtn.SetActive(false);
    }

    public void EnablePlayerHandInteractionBtns()
    {
        _submitTrickBtn.SetActive(true);
        _endTurnBtn.SetActive(true);
    }


}
