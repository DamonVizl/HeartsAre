using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// intermediary between decks/hands/etc. pushes UI events as well
/// </summary>
public class CardManager : MonoBehaviour
{
    [SerializeField] Deck _drawPile;
    [SerializeField] PlayerHand _hand;
    [SerializeField] List<Meld> _melds;
    [SerializeField] DiscardPile _discardPile; 
    PlayStateMachine _psm;
    SuperDefenderManager _superDefenderManager;
    TrickScorer _trickScorer;



    #region Events
    public static event Action<Card> OnCardAddedToHandFromDrawPile, OnCardDiscardedFromHand;
    public static event Action<int> OnTrickScore;
    #endregion
    private void OnEnable()
    {
        UI_DrawPile.OnDrawDeckClicked += DrawCardFromDeckToHand;
        UI_DiscardCardsButton.OnDiscardCardButtonPressed += DiscardCardsFromHand;
        UI_Button_SubmitTrick.OnSubmitButtonPressed += SubmitTrick;
        _trickScorer = new TrickScorer();


        _psm = GameObject.FindFirstObjectByType<PlayStateMachine>(); //TODO: hacky, fix later. 
        _superDefenderManager = FindObjectOfType<SuperDefenderManager>();
    }
    private void OnDisable()
    {
        UI_DrawPile.OnDrawDeckClicked -= DrawCardFromDeckToHand;
        UI_DiscardCardsButton.OnDiscardCardButtonPressed -= DiscardCardsFromHand;
        UI_Button_SubmitTrick.OnSubmitButtonPressed -= SubmitTrick;


    }
    private void SubmitTrick()
    {
        if (GameManager.Instance.TricksPlayedThisTurn >= GameManager.Instance.MaxNumberOfTricksPlayablePerTurn)
        {
            UI_MessageManager.Instance.ShowMessage("Max tricks played this turn");
            return; //if too many tricks have been played, return
        }
        int score = _trickScorer.CalculateHand(_hand.GetCurrentlySelectedCards());
        //trick failed
        if (score == 0)
        {
            SFXManager.Instance.PlayFirstSound(SFXName.TrickFailed);
            return;
        }
        //trick succeeded
        else
        {
            SFXManager.Instance.PlayFirstSound(SFXName.TrickSucceeded);

            /*            List<Card> cardsToRemove = new List<Card>();

                        //remove the cards
                        foreach (Card card in _selectedCards)
                        {
                            cardsToRemove.Add(card);
                        }
                        Debug.Log("cards to remove" + cardsToRemove.Count);*/
            DiscardCards(_hand.GetCurrentlySelectedCards());
            //_hand.GetCurrentlySelectedCards().Clear();

            /*            foreach (Card card in cardsToRemove)
                        {
                            RemoveCard(card);
                        }*/
            /*            //have the cardManager discard hte cards from the hand to the discard pile
                        _cardManager.DiscardCards(_selectedCards);*/

            OnTrickScore?.Invoke(score);
            GameManager.Instance.TricksPlayedThisTurn++;
            //once max tricks have been played, 
            if (GameManager.Instance.TricksPlayedThisTurn == GameManager.Instance.MaxNumberOfTricksPlayablePerTurn)
            {
                UI_MessageManager.Instance.ShowMessage("Max tricks played this turn, select end turn");

            }
        }
    }
    /// <summary>
    /// takes the selected cards from the player hand and discards them
    /// </summary>
    private void DiscardCardsFromHand()
    {
        // Check if there is at least one card in hand before allowing discard
        if (_hand.GetPlayerHand().Length > 0)
        {
            // if selects the trash bin while in ChooseSuperDefender state
            if (_psm.GetCurrentState() == PlayState.ChooseSuperDefender)
            {
                List<Card> cardsToRemove = new List<Card>(_hand.GetCurrentlySelectedCards());
                if (cardsToRemove.Count > 0)
                {
                    Card cardForSuperDefender = cardsToRemove[0]; // get the card selected for the super defender sacrifice
                    DiscardCards(cardsToRemove);

                    if (_superDefenderManager?.GetSelectedDefender() != null)
                    {
                        if (cardForSuperDefender.GetCardValueAsInt() < 11)
                        {
                            return;
                        }
                        _superDefenderManager.GetSelectedDefender().ChangeToSuperDefender(cardForSuperDefender);
                        _psm.TransitionToPreviousState();
                    }
                }
            }
            else if(GameManager.Instance.DiscardsThisTurn >= GameManager.Instance.MaxDiscardsPerTurn) {
                UI_MessageManager.Instance.ShowMessage("Max discards exceeded"); 
                return;  
            }
            {
                List<Card> cardsToRemove = new List<Card>(_hand.GetCurrentlySelectedCards());
                if (cardsToRemove.Count > 0)
                {
                    DiscardCards(cardsToRemove);
                    GameManager.Instance.DiscardsThisTurn++; 
                    //_psm.TransitionToState(PlayState.HeartDefenders);
                }
            }
        }
        else
        {
            Debug.Log("No cards in hand to discard.");
        }
    }

    /// <summary>
    /// this is called locally, after a discard button press has occured and also after a successful trick (from player hand)
    /// </summary>
    /// <param name="cardsToRemove"></param>
    public void DiscardCards(List<Card> cardsToRemove)
    {
        foreach (Card card in cardsToRemove)
        {
            _discardPile.AddCard(card);
            OnCardDiscardedFromHand?.Invoke(card);
        }
        foreach (Card card in new List<Card>(cardsToRemove)) // Iterate over a copy to avoid modifying during removal
        {
            Debug.Log("removing" + card.Value);
            _hand.RemoveCard(card);
        }
    }

    /// <summary>
    /// takes a card from the draw pile and puts it in the hand
    /// </summary>
    private void DrawCardFromDeckToHand()
    {
        
        //only draw a card if in the player turn 
        if (_psm.GetCurrentState() == PlayState.PlayerTurn)
        {
            if (_hand.GetCurrentNumberOfCardsInCollection() >= _hand.MaxCollectionSize)
            {
                //if the hand is full, don't draw
                UI_MessageManager.Instance.ShowMessage("Hand full, cannot draw"); 
                return;  
            }
            //fill the hand with cards
            while (_hand.GetCurrentNumberOfCardsInCollection() < _hand.MaxCollectionSize)
            {
                //when the draw pile is empty, refill it with shuffled cards from the discard pile
                if (_drawPile.GetCurrentNumberOfCardsInCollection() == 0)
                {
                    Debug.Log("draw pile empty");
                    ReshuffleDiscardPileIntoDrawPile();
                }
                Card drawnCard = _drawPile.DrawCard();
                if (drawnCard == null) break; //if there was no card to be drawn, break from the while loop
                if (!_hand.AddCard(drawnCard))
                {
                    //if the card couldn't be added, return it back to the original. TODO: I don't think this is putting it back to original spot, it's putting it into the first empty (which may be...)
                    _drawPile.AddCard(drawnCard);
                }
            }            
        }
    }
    /// <summary>
    /// takes all the discard pile cards and adds them to the draw pile. Then reshuffles. 
    /// </summary>
    private void ReshuffleDiscardPileIntoDrawPile()
    {
        _drawPile.ClearCardArray(); 
        Card[] tempCards = _discardPile.GetAllCards(); 
        for(int i = 0; i < tempCards.Length; i++)
        {
            if (tempCards[i] != null)
            {
                Debug.Log(tempCards[i].Value + "added to draw pile");
                _drawPile.AddCard(tempCards[i]);
            }
        }

        _discardPile.ClearCardArray(); 
/*        //take all the cards from the discard pile
        List<Card> takenDiscardCards = _discardPile.TakeAllCards(); 
        
        foreach (Card card in takenDiscardCards)
        {
            Debug.Log($"adding {card} to draw pile"); 
            _drawPile.AddCard(card);
        }
*/
        //shuffle cards.
        _drawPile.ShuffleCards(); 
    }

    /// <summary>
    /// select a card from the player hand, can then do different things with it like place it in a meld, move it to a heart Defender position, etc
    /// </summary>
    public void SelectPlayerHandCard()
    {

    }

}
