using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class HeartDefender : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int heartRank;
    public TextMeshProUGUI rankCounter;
    [SerializeField] private float levelUpRateIncrease;
    [SerializeField] private GameManager gameManager;
    private PlayerHealth playerHealth;

    private const int maxLevel = 10;

    [SerializeField] private float _tweenValue = 1f;

    public Image upgradeArrowIcon;
    private float iconDisabledSaturation = .1f;

    [SerializeField] private PlayStateMachine playStateMachine;

    RectTransform parentRectTransform; // UI parent reference for camera shake

    public ParticleSystem deathParticle;

    private HeartDefenderManager heartDefenderManager;

    bool _defenderSelected = false;
    Tween _tween;

    private Vector3 _originalPosition;

    private HorizontalLayoutGroup layoutGroup;


    private void Start()
    {
        layoutGroup = GetComponentInParent<HorizontalLayoutGroup>();
        gameManager = FindObjectOfType<GameManager>();
        playerHealth = GameManager.Instance.PlayerHealth;
        UpdateRankCounter_UI();
        DisableIcon();
        playStateMachine = FindObjectOfType<PlayStateMachine>();
        parentRectTransform = GetComponent<RectTransform>().parent.GetComponent<RectTransform>();

        heartDefenderManager = FindObjectOfType<HeartDefenderManager>();

        _originalPosition = transform.position;

    }

    private void Update()
    {
        CheckIfCanUpgrade();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (playStateMachine.GetCurrentState() == PlayState.EnemyTurn)
        {
            if (!_defenderSelected)
            {
                SelectDefender();
            }
            else
            {
                UnSelectDefender();
            }
        }
    }

    private void SelectDefender()
    {
        if (_tween.IsActive()) return;

        // disable horizontal layout so tweens can operate
        DisableLayoutGroup();

        heartDefenderManager.AddDefenderForAttack(this);
        _tween = transform.DOMoveY(transform.position.y + _tweenValue, 0.2f);
      

        _defenderSelected = true;
    }

    // deactivates horizontal layout group for defenders so tweening can operate properly
    void DisableLayoutGroup()
    {
        if (layoutGroup != null)
        {
            layoutGroup.enabled = false;
        }

    }

    // activates horizontal layout group for defenders
    void EnableLayoutGroup()
    {
        if (layoutGroup != null)
        {
            layoutGroup.enabled = true;
        }
    }


    private void UnSelectDefender()
    {
        if (_tween.IsActive()) return;

        heartDefenderManager.RemoveDefenderForAttack(this);
        _tween = transform.DOMoveY(transform.position.y - _tweenValue, 0.2f);

        _defenderSelected = false;
    }

    // reset defender position and turn re-enable layout group
    public void ResetPosition()
    {
        EnableLayoutGroup();
        transform.position = new Vector2(transform.position.x, _originalPosition.y);
        _defenderSelected = false;
    }

    // updates the badge counter
    public void UpdateRankCounter_UI()
    {
        rankCounter.text = heartRank.ToString();
    }

    // if player tries to purchase a rank upgrade and has enough currency, upgrade the card's rank
    public void PurchaseRankUpgrade()
    {
        if (IsAbleToUpgrade())
        {
            UpgradeRank(GetNextUpgradeCost());
        }
        else
        {
            Debug.Log("You do not have enough money to upgrade this heart or it's at max level");
            ShakeCamera();
        }
    }

    // if heart takes enough damage that surpasses it's rank's damage threshold, decrease its rank
    public void TakeDamage(int damage)
    {
        if (deathParticle != null)
        {
            deathParticle.Play();
        }
        ShakeCamera();
        // track damage amt that surpasses defender's rank
        int overDamage = 0;
        // if damage amount is higher than the defender's, decrease the rank of the heart by the difference between the amount of damage and the heart's rank
        if (damage > heartRank)
        {
            overDamage = damage - heartRank;
            DecreaseRank(overDamage);
        }
    }

    void DecreaseRank(int damage)
    {
        // tracks overkill damage
        int overKillDmg = 0;

        heartRank -= damage;

        overKillDmg = Mathf.Abs(heartRank);

        UpdateRankCounter_UI();

        // if heartRank is 0 or lower, destroy it and apply overkill damage to player
        if (heartRank <= 0)
        {
            DestroyHeart(overKillDmg);
        }
    }

    void UpgradeRank(int cost)
    {
        CurrencyManager.RemoveMoney(cost);
        heartRank++;
        // add SubtractDifferenceToPlayer()
        UpdateRankCounter_UI();
    }

    void OverKillDamage(int damage)
    {
        // if this heart dies, player will receive overkill damage
        playerHealth.RemoveHealth(damage);
    }

    void DestroyHeart(int damage)
    {
        heartDefenderManager.RemoveFromDefenderList(this);
        heartDefenderManager.RemoveDefenderForAttack(this);
        // apply overkill
        OverKillDamage(damage);
        // destroy this heart
        Destroy(this.gameObject);
    }

    // shows upgrade arrow in transparent, inactive state
    public void DisableIcon()
    {
        Color iconColor = upgradeArrowIcon.color;
        iconColor.a = iconDisabledSaturation;
        upgradeArrowIcon.color = iconColor;  
    }

    // show upgrade arrow in active state with full saturation
    void EnableIcon()
    {
        Color iconColor = upgradeArrowIcon.color;
        iconColor.a = 1f;
        upgradeArrowIcon.color = iconColor;
    }


    // calculates this heart defender's next upgrade cost
    private int GetNextUpgradeCost()
    {
        int levelUpCost = 0;
        levelUpCost = Mathf.RoundToInt(heartRank * levelUpRateIncrease);

        return levelUpCost;
    }


    // checks if player has enough money to upgrade this heart defender and is below max level (10)
    public bool IsAbleToUpgrade()
    {
        if (CurrencyManager.GetCurrentMoney() >= GetNextUpgradeCost() && heartRank < maxLevel && playStateMachine.GetCurrentState() == PlayState.PlayerTurn)
        {
            return true;
        }
        return false;
    }

    // checks if heart defender is upgradable at runtime
    void CheckIfCanUpgrade()
    {
        if (playStateMachine != null)
        {
            if (playStateMachine.GetCurrentState() == PlayState.PlayerTurn)
            {
                if (IsAbleToUpgrade())
                {
                    EnableIcon();
                }
                else
                {
                    DisableIcon();
                }
            }
        }
    }
    // use this to upgrade to super defender
    private void ChangeToSuperDefender()
    {

    }

    private void ShakeCamera()
    {
        CameraShake cameraShake = Camera.main.GetComponent<CameraShake>();

        if (cameraShake != null && parentRectTransform != null)
        {
            cameraShake.StartCameraShake(.2f, .5f, parentRectTransform); // shake camera to signal unable to perform action
        }
    }

}
