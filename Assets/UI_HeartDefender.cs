using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class UI_HeartDefender : MonoBehaviour
{
    RectTransform parentRectTransform; // UI parent reference for camera shake
    public GameObject attackIcon;
    public TextMeshProUGUI dmgCounter;
    public TextMeshProUGUI rankCounter;
    public HeartDefender heartDefender;
    public TextMeshProUGUI bufferCounter;

    public float _damageAnimateSpeed;

    public float tweenDownDistance;
    public float tweenDuration;
    public float pauseDuration;

    public Sprite _superDefenderUpgradeIconSprite;
    public Sprite _upgradeArrowSprite;
    public Sprite _cancelUpgradeSprite;
    public Image upgradeArrowIcon;

    [SerializeField] private PlayStateMachine playStateMachine;



    // Start is called before the first frame update
    void Start()
    {
        playStateMachine = FindObjectOfType<PlayStateMachine>();
        parentRectTransform = GetComponent<RectTransform>().parent.GetComponent<RectTransform>();
        //dmgCounter.gameObject.SetActive(false);
        attackIcon.SetActive(false);
        heartDefender = GetComponent<HeartDefender>();
        UpdateRankUI(heartDefender.BaseHeartRank());
    }


    public void UpdateRankUI(int rank) => rankCounter.text = rank.ToString();
    public void UpdateBufferUI(int value) => bufferCounter.text = "+" + value.ToString();

    public void ShowDamage(int damage)
    {
        StartCoroutine(AnimateDamage(damage));   
    }

    private IEnumerator AnimateDamage(int rawDamage)
    {
        dmgCounter.text = rawDamage.ToString();
        //dmgCounter.gameObject.SetActive(true);
        attackIcon.SetActive(true);
        int heartRank = heartDefender.BaseHeartRank();


        int rankMinusDmg = heartRank - rawDamage;
        
        int targetDisplayValue = Mathf.Min(rankMinusDmg, 0);
        yield return new WaitForSeconds(pauseDuration);

        Vector3 originalPosition = attackIcon.transform.localPosition;
        Vector3 downPosition = originalPosition - new Vector3(0, tweenDownDistance, 0);
        attackIcon.transform.DOLocalMove(downPosition, tweenDuration).SetEase(Ease.OutQuad);

        yield return new WaitForSeconds(tweenDuration);


        for (int i = rawDamage; i >= targetDisplayValue; i--)
        {
            dmgCounter.text = i.ToString();
            yield return new WaitForSeconds(0.05f);
        }


        attackIcon.transform.DOLocalMove(originalPosition, tweenDuration).SetEase(Ease.InQuad);

        yield return new WaitForSeconds(tweenDuration);

        attackIcon.gameObject.SetActive(false);

        if (rankMinusDmg < 0)
        {
            StartCoroutine(AnimateRankDecrease(Mathf.Abs(rankMinusDmg), heartRank));
        }

    }

    public void ShowRankDecrease(int damage, int rank)
    {
        StartCoroutine(AnimateRankDecrease(damage, rank));
    }

    private IEnumerator AnimateRankDecrease(int damage, int heartRank)
    {
        int currentRank = heartRank;
        for (int i = damage; i > 0; i--)
        {
            currentRank--;
            UpdateRankUI(currentRank);
            yield return new WaitForSeconds(0.05f);
        }
    }


    public void ShowBufferCounter(int numOfKingsInPlay)
    {
        bufferCounter.gameObject.SetActive(true);
        UpdateBufferUI(numOfKingsInPlay);
    }

    public void HideBufferCounter()
    {
        bufferCounter.gameObject.SetActive(false);
    }

    public void SetSiblingIndex(int index) => transform.SetSiblingIndex(index);


    public void ShakeCamera()
    {
        CameraShake cameraShake = Camera.main.GetComponent<CameraShake>();

        if (cameraShake != null && parentRectTransform != null)
        {
            cameraShake.StartCameraShake(.2f, .5f, parentRectTransform); // shake camera to signal unable to perform action
        }
    }

    public void SwitchUpgradeIcon()
    {
        if (playStateMachine.GetCurrentState() == PlayState.ChooseSuperDefender)
        {
            upgradeArrowIcon.sprite = _cancelUpgradeSprite;
        }
        else
        {
            if (heartDefender.BaseHeartRank() == heartDefender.GetMaxLevel())
            {
                upgradeArrowIcon.sprite = _superDefenderUpgradeIconSprite;
            }
            else
            {
                upgradeArrowIcon.sprite = _upgradeArrowSprite;
            }
        }
    }
}