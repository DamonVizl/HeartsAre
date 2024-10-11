using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using System; 

/// <summary>
/// sits on the UI Deck, pushes clicked events
/// </summary>
public class UI_DrawDeck : MonoBehaviour, IPointerDownHandler
{
    public static event Action OnDrawDeckClicked; 
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Clicked");
        OnDrawDeckClicked?.Invoke();
    }
    //TODO: make a fancy shuffle animation
    //TODO: make a fancy restock animation (after drawing)
    //TODO: make a draw animation
}
