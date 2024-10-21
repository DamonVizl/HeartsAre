using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UseNoDefenderOption : MonoBehaviour
{
    public Button button;

    private void Start()
    {
        button.onClick.AddListener(OnSelected);
    }

    private void OnSelected()
    {
        GameManager.PlayerSelectsNoDefender(this);
    }
}
