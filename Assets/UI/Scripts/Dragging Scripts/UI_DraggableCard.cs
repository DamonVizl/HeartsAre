using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Card))]
public class UI_DraggableCard : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    Transform _originalParent;
    RectTransform _rectTransform;
    private CanvasGroup _canvasGroup; 
    
    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Start dragging card");
        _originalParent = transform.parent;
        _canvasGroup.blocksRaycasts = false; //allow raycasts to find the drop area
        _canvasGroup.alpha = 0.7f; //make the card slightly transparent
    }

    //begin drag
    public void OnDrag(PointerEventData eventData)
    {
        //update the cards position to the mouse position
        _rectTransform.anchoredPosition += eventData.delta /_canvasGroup.transform.lossyScale; 
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        UI_CardSlot validDropSlot = eventData.pointerEnter.transform.GetComponent<UI_CardSlot>(); 
        if(validDropSlot != null && !validDropSlot.HasCardInSlot()) //add a check for if there is already a card here
        {
            //set the new parent
            transform.SetParent(eventData.pointerEnter.transform, false);
            //add the card to the slot
            validDropSlot.SetCardInSlot(this.GetComponent<Card>()); 
        }
        else //cannot drop the card here, return it to it's original pos
        {
            transform.SetParent(_originalParent, false);
            _rectTransform.anchoredPosition = transform.parent.localPosition;

            //re-enable raycasts and make it full transparency
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.alpha = 1f; 
        }
    }
} 
