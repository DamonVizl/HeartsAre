using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_StartMenu : MonoBehaviour
{
    public Button button;

    private void Start()
    {
        button.onClick.AddListener(OnSelected);
    }

    private void OnSelected()
    {
        if (MenuManager.Instance != null)
        {
            MenuManager.Instance.HideStartMenu();
        }

    }

   
}
