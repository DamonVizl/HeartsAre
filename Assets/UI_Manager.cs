using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private List<GameObject> playerHandCards_UI;
    private PlayerHand playerHand;

    private void Start()
    {
        playerHand = FindObjectOfType<PlayerHand>();
    }

    // refreshes the UI card gameObjects to reflect what's in the player's hand
    public void RefreshHandUI()
    {
        int playerHandSize = playerHand.GetPlayerHand().Count;

        for (int i = 0; i < playerHandSize; i++)
        {
            Card card = playerHand.GetPlayerHand()[i];

            Card card_UI = playerHandCards_UI[i].GetComponent<Card>();

            Image cardImage = playerHandCards_UI[i].GetComponent<Image>();

            cardImage.sprite = card.GetCardSprite();
            card_UI.SetCardValues(card.GetCardSuit(), card.GetCardValue(), card.GetCardSprite());
        }
    }

    private void Update()
    {
        // temporary implementation for testing player hand
        if (Input.GetMouseButtonDown(0))
        {
            RefreshHandUI();
        }
    }

}
