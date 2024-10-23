using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;


public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    private UI_StartMenu _ui_StartMenu;
    [SerializeField] private GameObject _startMenuObj;

    public static event Action<UI_StartMenu> OnStartGameSelected;

    private void Start()
    {
        UI_StartMenu _ui_StartMenu = FindObjectOfType<UI_StartMenu>();
        //_startMenuObj = _ui_StartMenu.gameObject;
        //ShowStartMenu();
    }

    private void OnEnable()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    public void ShowStartMenu()
    {
        _startMenuObj.SetActive(true);
    }

    public void HideStartMenu()
    {
        _startMenuObj.SetActive(false);
    }
}
