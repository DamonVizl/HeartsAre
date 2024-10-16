using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UI_ScorePopup : MonoBehaviour
{
    TextMeshProUGUI _TMPText;
    Vector3 _originalPosition; 
    private void OnEnable()
    {
        TrickScorer.OnTrickScored += HandleTrickScored; 
        _TMPText = GetComponent<TextMeshProUGUI>();
        _originalPosition = transform.position;
    }

    private void OnDisable()
    {
        TrickScorer.OnTrickScored -= HandleTrickScored;
    }
    private void HandleTrickScored(string message, int score)
    {
        _TMPText.text = message;
        
    }
}
