using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_HandsDrawnCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _handsDrawnCounter; 
    // Start is called before the first frame update
    private void OnEnable()
    {
        GameManager.OnCardsDiscardedValueChanged += UpdateTrickPlayed; 
    }
    private void OnDisable()
    {
        _handsDrawnCounter.text = string.Empty;
        GameManager.OnCardsDiscardedValueChanged -= UpdateTrickPlayed;
    }
    private void UpdateTrickPlayed(int handsDrawn)
    {
        _handsDrawnCounter.text = $"{handsDrawn}/{GameManager.Instance.MaxDiscardsPerTurn}"; 
    }
}
