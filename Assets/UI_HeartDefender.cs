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
        dmgCounter.text = damage.ToString();
        dmgCounter.gameObject.SetActive(true);

        StartCoroutine(AnimateDamageText(damage, heartDefender.TotalHeartRank()));
    }
    private IEnumerator AnimateDamageText(int damage, int targetRank)
    {

        int rankMinusDmg = heartDefender.TotalHeartRank() - damage;
        int targetDisplayValue = Mathf.Min(rankMinusDmg, 0);
        int currentDisplayValue = damage;
        dmgCounter.text = currentDisplayValue.ToString();

        yield return new WaitForSeconds(0.5f); // Slight pause after initial display

        while (currentDisplayValue > targetDisplayValue)
        {
            currentDisplayValue -= 1;
            dmgCounter.text = currentDisplayValue.ToString();
            yield return new WaitForSeconds(_damageAnimateSpeed);
        }

        dmgCounter.text = currentDisplayValue.ToString(); // Ensure final display is the target rank
        yield return new WaitForSeconds(.5f);
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
