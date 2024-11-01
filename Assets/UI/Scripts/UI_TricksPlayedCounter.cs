using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_TricksPlayedCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _tricksCounter; 
    // Start is called before the first frame update
    private void OnEnable()
    {
        GameManager.OnTrickPlayedValueChanged += UpdateTrickPlayed; 
    }
    private void OnDisable()
    {
        _tricksCounter.text = string.Empty;
        GameManager.OnTrickPlayedValueChanged -= UpdateTrickPlayed;
    }
    private void UpdateTrickPlayed(int tricksPlayed)
    {
        _tricksCounter.text = $"{tricksPlayed}/{GameManager.Instance.MaxNumberOfTricksPlayablePerTurn}"; 
    }
}
