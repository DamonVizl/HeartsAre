using DG.Tweening;
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
        _TMPText.text = message + " + " + score + " points";
        transform.position = _originalPosition;
        _TMPText.enabled = true;
        transform.DOScale(0.8f, 0.5f);
        _TMPText.CrossFadeAlpha(0.2f, 0.5f, false);
        transform.DOLocalMoveY(transform.position.y + Random.Range(20f, 40f), 0.5f).OnComplete(() =>
        {
            //disable the text when done. 
            _TMPText.enabled = false;
            transform.position = _originalPosition;
            _TMPText.transform.localScale = new Vector3(1, 1, 1);
            _TMPText.alpha = 255f;

        });

    }
}
