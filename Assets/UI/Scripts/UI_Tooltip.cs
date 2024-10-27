using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string _headerText; 
    [SerializeField] private string _bodyText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(ShowUI(eventData)); 
    }
    private IEnumerator ShowUI(PointerEventData eventData)
    {
        yield return new WaitForSeconds(1f); 
        RectTransform rectTransform = GetComponent<RectTransform>();
        Vector2 pos = rectTransform.position + new Vector3(rectTransform.rect.width, rectTransform.rect.height, 0); 
        if(rectTransform != null ) UI_TooltipManager.Instance.ShowTooltip(_headerText, _bodyText, pos);
        //else use mouse pos
        else UI_TooltipManager.Instance.ShowTooltip(_headerText, _bodyText, eventData.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        UI_TooltipManager.Instance.HideTooltip();
    }
}
