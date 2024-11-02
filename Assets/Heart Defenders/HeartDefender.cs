using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class HeartDefender : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int heartRank;
    public int fixedCost;

    [SerializeField] private float levelUpRateIncrease;
    private PlayerHealth playerHealth;

    private const int maxLevel = 10;

    public Image upgradeArrowIcon;

    [SerializeField] private PlayStateMachine playStateMachine;

    public ParticleSystem deathParticle;

    private HeartDefenderManager heartDefenderManager;

    bool _defenderSelected = false;
    private Vector3 _originalPosition;
    public Sprite _superDefenderUpgradeIconSprite;
    public Sprite _upgradeArrowSprite;
    public Sprite _cancelUpgradeSprite;

    private SuperDefenderManager _superDefenderManager;
    [SerializeField] private Image _defenderSprite;

    [SerializeField] private GameObject _upgradeIcon_Obj;
    [SerializeField] private GameObject _rankCounter_Obj;

    private UI_PlayerHand _ui_playerHand;


    public UI_HeartDefender _ui_heartDefender;
    public Animator _animator;

    private void Start()
    {
        _ui_heartDefender = GetComponent<UI_HeartDefender>();
        playerHealth = GameManager.Instance.PlayerHealth;
        playStateMachine = FindObjectOfType<PlayStateMachine>();

        heartDefenderManager = FindObjectOfType<HeartDefenderManager>();

        _originalPosition = transform.position;
        _superDefenderManager = FindObjectOfType<SuperDefenderManager>();

        _ui_playerHand = FindObjectOfType<UI_PlayerHand>();
        _animator = GetComponentInChildren<Animator>();

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (playStateMachine.GetCurrentState() == PlayState.HeartDefenders)
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

        transform.SetParent(heartDefenderManager.battleGroundContainer);
        heartDefenderManager.AddDefenderForAttack(this);
        _defenderSelected = true;
        SFXManager.Instance.PlayRandomSound(SFXName.SelectDefender);
    }


    private void UnSelectDefender()
    {
        ResetPosition();
        heartDefenderManager.RemoveDefenderForAttack(this);
        _defenderSelected = false;
        SFXManager.Instance.PlayRandomSound(SFXName.CardDeselect);

    }

    // reset defender position and turn re-enable layout group
    public void ResetPosition()
    {
        transform.SetParent(heartDefenderManager.cardContainer);
        _ui_heartDefender.SetSiblingIndex(0);
        _defenderSelected = false;
    }

    // if player tries to purchase a rank upgrade and has enough currency, upgrade the card's rank
    public void PurchaseRankUpgrade()
    {
        if (IsAbleToUpgrade())
        {
            UpgradeDefender(GetNextUpgradeCost());

        }
        else
        {
            Debug.Log("You do not have enough money to upgrade this heart or it's at max level");
            SFXManager.Instance.PlayRandomSound(SFXName.PurchaseFailed);
            _ui_heartDefender.ShakeCamera();
        }
    }

    void UpgradeDefender(int cost)
    {
        // if player is already in ChooseSuperDefender state and clicks the X icon on defender, transition back to player state and switch back the icon
        if (playStateMachine.GetCurrentState() == PlayState.ChooseSuperDefender)
        {
            playStateMachine.TransitionToPreviousState();
            SwitchUpgradeIcon();
        }
        else
        {
            // if defender is at the max level, is not selected for defense this round, and max super defenders haven't been reached, then check if super defender is upgrade is plausible
            if (BaseHeartRank() >= maxLevel && !_defenderSelected && _superDefenderManager.ActiveSuperDefenders.Count < _superDefenderManager.GetMaxSuperDefenders())
            {
                CheckForSuperDefender();
            }
            else if (BaseHeartRank() < maxLevel)
            {
                UpgradeRank(cost);
            }
            else
            {
                // play error fx
                SFXManager.Instance.PlayRandomSound(SFXName.PurchaseFailed);
                _ui_heartDefender.ShakeCamera();
            }
        }
    }

    void UpgradeRank(int cost)
    {
        CurrencyManager.RemoveMoney(cost);
        IncreaseHeartRank();
        // add SubtractDifferenceToPlayer()
        _ui_heartDefender.UpdateRankUI(BaseHeartRank());
        //play the sfx 
        SFXManager.Instance.PlaySoundAtIndex(SFXName.LevelUpDefender, heartRank);


        // check if the upgrade icon needs to change to the superdefender icon
        if (BaseHeartRank() == maxLevel)
        {
            SwitchUpgradeIcon();
        }
    }

    private void CheckForSuperDefender()
    {
        playStateMachine.TransitionToState(PlayState.ChooseSuperDefender);
        _superDefenderManager.SetSelectedHeartDefender(this);
        _ui_playerHand.DisablePlayerHandInteractionBtns();
        SwitchUpgradeIcon(); // change upgrade icon to the X so player can cancel
    }

    // use this to upgrade to super defender
    public void ChangeToSuperDefender(Card card)
    {

        // Step 1: Instantiate a new GameObject for the SuperDefender
        GameObject superDefenderObj = new GameObject("SuperDefender");
        //superDefenderObj.transform.SetParent(_superDefenderManager.SuperDefenderPanel.transform, false); // Set as child of SuperDefenderPanel
        SuperDefender superDefender = superDefenderObj.AddComponent<SuperDefender>();

        // Step 2: Initialize the new SuperDefender component
        Image superDefenderImage = superDefenderObj.AddComponent<Image>(); // Add an Image component for the defender icon
        superDefender.InitializeFromCard(card, superDefenderImage);

        // Step 3: Add to SuperDefenderManager
        _superDefenderManager.AddSuperDefender(superDefender);
        _superDefenderManager.ClearSelectedDefender();

        // Step 4: Apply any immediate effects for specific types
        if (superDefender.superDefenderType == SuperDefender.SuperDefenderType.King)
        {
            superDefender.ApplyPassiveEffect();
        }

        // Step 5: Clean up the current object
        heartDefenderManager.RemoveFromDefenderList(this);
        Destroy(this.gameObject); // Remove this component

        // Re-enable player hand interaction buttons
        _ui_playerHand.EnablePlayerHandInteractionBtns();

        // Step 6: Check for win state after adding the SuperDefender
        if (_superDefenderManager.SuperDefenderWinStateMet())
        {
            playStateMachine.TransitionToState(PlayState.Win);
        }
    }



    // checks if player has enough money to upgrade this heart defender and is below max level (10)
    public bool IsAbleToUpgrade()
    {
        if (CurrencyManager.GetCurrentMoney() >= GetNextUpgradeCost() && playStateMachine.GetCurrentState() != PlayState.EnemyTurn)
        {
            return true;
        }
        return false;
    }

    // if heart takes enough damage that surpasses it's rank's damage threshold, decrease its rank
    public void TakeDamage(int attackPower)
    {
        _ui_heartDefender.ShakeCamera();
        // track damage amt that surpasses defender's rank
        int actualDamage = 0;
        _ui_heartDefender.ShowDamage(attackPower);
        // if damage amount is higher than the defender's, decrease the rank of the heart by the difference between the amount of damage and the heart's rank
        if (attackPower > TotalHeartRank())
        {
            actualDamage = Mathf.Abs(TotalHeartRank() - attackPower);
            Debug.Log("actual damage: " + actualDamage);
            DamageDefender(actualDamage);
            //play the sound for the player taking damage
            SFXManager.Instance.PlayRandomSound(SFXName.DestroyDefender);
        }
        else
        {
            //play the sound for when the defender succesffuly defends
            SFXManager.Instance.PlayRandomSound(SFXName.DefenderDefends);
        }
    }

    void DamageDefender(int damage)
    {
        // tracks overkill damage
        int totalDamage = damage;

        int currentHeartRank = TotalHeartRank();
        _ui_heartDefender.ShowRankDecrease(totalDamage, BaseHeartRank());
        DecreaseHeartRank(totalDamage);

        // if heartRank is 0 or lower, destroy it and apply overkill damage to player
        if (TotalHeartRank() <= 0)
        {
            ApplyOverKillDamage(totalDamage);
            DestroyHeart();
           
        }

        // check if need to revert upgrade icon back to arrow icon
        if (TotalHeartRank() < maxLevel)
        {
            SwitchUpgradeIcon();
        }
    }
    public void DestroyHeart()
    {
        heartDefenderManager.RemoveFromDefenderList(this);
        heartDefenderManager.RemoveDefenderForAttack(this);
        // apply overkill damage to palyer

        // destroy this heart
        _animator.SetTrigger("Death");
        SFXManager.Instance.PlayRandomSound(SFXName.DefenderKilled);
    }

    void ApplyOverKillDamage(int damage)
    {
 
        // if this heart dies, player will receive overkill damage
        playerHealth.RemoveHealth(damage);
        SFXManager.Instance.PlayRandomSound(SFXName.PlayerTakeDamage);
    }


    // calculates this heart defender's next upgrade cost
    private int GetNextUpgradeCost()
    {
        int levelUpCost = 0;
        levelUpCost = Mathf.RoundToInt(BaseHeartRank() * fixedCost);

        return levelUpCost;
    }

    public void PlayParticleEffect()
    {
        if (deathParticle != null)
        {
            deathParticle.Play();
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
            if (BaseHeartRank() == maxLevel)
            {
                upgradeArrowIcon.sprite = _superDefenderUpgradeIconSprite;
            }
            else
            {
                upgradeArrowIcon.sprite = _upgradeArrowSprite;
            }
        }
    }

    // returns the total heart rank of the defender + any superdefender of type Kings in play
    public int TotalHeartRank()
    {
        int totalHeartRank = BaseHeartRank();
        if (_superDefenderManager != null)
        {
            int numberOfKingSuperdefendersInPlay = _superDefenderManager.GetKingDefenders().Count;
            totalHeartRank = BaseHeartRank() + numberOfKingSuperdefendersInPlay;
        }

        return totalHeartRank;
    }


    // actual heartRank of the defender without including buffs
    public int BaseHeartRank()
    {
        return heartRank;
    }

    public void CheckAndShowBuffer()
    {
        if (_superDefenderManager != null && _superDefenderManager.GetKingDefenders().Count > 0)
        {
            _ui_heartDefender.ShowBufferCounter(_superDefenderManager.GetKingDefenders().Count);
        }
    }

    public void IncreaseHeartRank()
    {
        heartRank++;
    }

    public void DecreaseHeartRank(int value)
    {
        heartRank -= value;
    }



}