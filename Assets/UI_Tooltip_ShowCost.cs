using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Tooltip_ShowCost : UI_Tooltip
{
    private HeartDefender _heartDefender; // Replace with the actual class name
    [SerializeField] private string _altHeaderText;
    [SerializeField] private string _altBodyText;

    private void Awake()
    {
        _heartDefender = GetComponentInParent<HeartDefender>();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(ShowUI(eventData));
    }

    protected override IEnumerator ShowUI(PointerEventData eventData)
    {
        yield return new WaitForSeconds(1f);

        // Replace the placeholder in _bodyText with the current cost
        string updatedBodyText = string.Format(_bodyText, _heartDefender.GetNextUpgradeCost());
        string bodyTextToShow = _heartDefender != null && _heartDefender.TotalHeartRank() == 10 ? _altBodyText : updatedBodyText;
        string headerTextToShow = _heartDefender != null && _heartDefender.TotalHeartRank() == 10 ? _altHeaderText : _headerText;

        RectTransform rectTransform = GetComponent<RectTransform>();
        Vector2 pos = rectTransform.position + new Vector3(rectTransform.rect.width / 2, rectTransform.rect.height / 2, 0);

        if (rectTransform != null)
            UI_TooltipManager.Instance.ShowTooltip(headerTextToShow, bodyTextToShow, pos);
        else
            UI_TooltipManager.Instance.ShowTooltip(headerTextToShow, bodyTextToShow, eventData.position);
    }
}
