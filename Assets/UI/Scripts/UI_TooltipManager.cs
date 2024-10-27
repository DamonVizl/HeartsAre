using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_TooltipManager : MonoBehaviour
{
    public static UI_TooltipManager Instance;
    private Image _tooltip;
    [SerializeField] private TextMeshProUGUI _headerText;
    [SerializeField] private TextMeshProUGUI _bodyText;
    private void OnEnable()
    {
        if (Instance == null)
        {
            Instance = this;
            _tooltip = GetComponent<Image>();
        }
        else Destroy(this); 
    }

    public void ShowTooltip(string headerText, string bodyText, Vector2 screenPosition)
    {

        _tooltip.transform.position = screenPosition;
        _headerText.text = headerText;
        _bodyText.text = bodyText;   
        _bodyText.enabled = true;
        _headerText.enabled = true; 
        _tooltip.enabled = true;
    }
    public void HideTooltip()
    {
        _tooltip.enabled = false;
        _bodyText.enabled = false;
        _headerText.enabled = false;


    }

}
