using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ConfirmSelection : MonoBehaviour, IPointerClickHandler
{
    public HeartDefenderManager _heartDefenderManager;

    private void Start()
    {
        _heartDefenderManager = FindObjectOfType<HeartDefenderManager>();
    }

    private void OnSelected()
    {
        if (_heartDefenderManager != null)
        {
            _heartDefenderManager.PlayerConfirmsSelection(this);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnSelected();
    }
}
