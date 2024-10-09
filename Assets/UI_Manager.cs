using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private List<GameObject> playerHandCards_UI;
    private PlayerHand playerHand;
    public GameObject cardPrefab_UI;
    public Transform HandUI;

    private void Start()
    {
        playerHand = FindObjectOfType<PlayerHand>();
    }

    public void RefreshHandUI()
    {
        // Clear the current UI elements (assuming playerHandCards_UI is a List<GameObject>)
        foreach (Transform child in HandUI)
        {
            Destroy(child.gameObject);
        }

        // Get the player's hand
        List<Card> playerHandCards = playerHand.GetPlayerHand();
        int playerHandSize = playerHandCards.Count;

        // Instantiate UI elements for the player's hand
        for (int i = 0; i < playerHandSize; i++)
        {
            Card card = playerHandCards[i];

            // Instantiate the card UI prefab
            GameObject cardUI = Instantiate(cardPrefab_UI, HandUI.transform); // Ensure playerHandCards_UI is the parent

            // Set the sprite and values for the card
            Image cardImage = cardUI.GetComponent<Image>();
            cardImage.sprite = card.GetCardSprite();

            Card cardComponent = cardUI.GetComponent<Card>();
            cardComponent.SetCardValues(card.GetCardSuit(), card.GetCardValue(), card.GetCardSprite());
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
