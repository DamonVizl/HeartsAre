using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class UI_HeartDefender : MonoBehaviour
{
    RectTransform parentRectTransform; // UI parent reference for camera shake
    public TextMeshProUGUI dmgCounter;
    public TextMeshProUGUI rankCounter;
    public HeartDefender heartDefender;

 
    public float _damageAnimateSpeed;

    public float tweenDownDistance;
    public float tweenDuration;
    public float pauseDuration;

    // Start is called before the first frame update
    void Start()
    {
        parentRectTransform = GetComponent<RectTransform>().parent.GetComponent<RectTransform>();
        dmgCounter.gameObject.SetActive(false);
        heartDefender = GetComponent<HeartDefender>();
        UpdateRankUI();
    }


    public void UpdateRankUI() => rankCounter.text = heartDefender.BaseHeartRank().ToString();

    public void ShowDamage(int damage)
    {
        StartCoroutine(AnimateDamage(damage));
    }

    private IEnumerator AnimateDamage(int rawDamage)
    {
        dmgCounter.text = rawDamage.ToString();
        dmgCounter.gameObject.SetActive(true);


        int rankMinusDmg = heartDefender.TotalHeartRank() - rawDamage;
        int targetDisplayValue = Mathf.Min(rankMinusDmg, 0);

        yield return new WaitForSeconds(pauseDuration);

        Vector3 originalPosition = dmgCounter.transform.localPosition;
        Vector3 downPosition = originalPosition - new Vector3(0, tweenDownDistance, 0);
        dmgCounter.transform.DOLocalMove(downPosition, tweenDuration).SetEase(Ease.OutQuad);

        yield return new WaitForSeconds(tweenDuration);


        for (int i = rawDamage; i >= targetDisplayValue; i--)
        {
            dmgCounter.text = i.ToString();
            yield return new WaitForSeconds(0.05f); // Adjust delay to control countdown speed
        }

        dmgCounter.transform.DOLocalMove(originalPosition, tweenDuration).SetEase(Ease.InQuad);

        yield return new WaitForSeconds(tweenDuration);

        dmgCounter.gameObject.SetActive(false);

    }


    public void ShakeCamera()
    {
        CameraShake cameraShake = Camera.main.GetComponent<CameraShake>();

        if (cameraShake != null && parentRectTransform != null)
        {
            cameraShake.StartCameraShake(.2f, .5f, parentRectTransform); // shake camera to signal unable to perform action
        }
    }
}
