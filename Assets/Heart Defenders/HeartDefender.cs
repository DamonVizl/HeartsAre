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
    [SerializeField] private GameManager gameManager;
    private PlayerHealth playerHealth;

    private const int maxLevel = 10;

    public Image upgradeArrowIcon;

    [SerializeField] private PlayStateMachine playStateMachine;

    public ParticleSystem deathParticle;

    private HeartDefenderManager heartDefenderManager;

    bool _defenderSelected = false;
    private Vector3 _originalPosition;

    public bool isSuperDefender;

    public Sprite _superDefenderUpgradeIconSprite;
    public Sprite _upgradeArrowSprite;
    public Sprite _cancelUpgradeSprite;

    private SuperDefenderManager _superDefenderManager;
    [SerializeField] private Image _defenderSprite;

    [SerializeField] private GameObject _upgradeIcon_Obj;
    [SerializeField] private GameObject _rankCounter_Obj;

    private UI_HeartDefenderInteractions _ui_heartDefenderInteractions;
    private UI_PlayerHand _ui_playerHand;


    public UI_HeartDefender _ui_heartDefender;

    private void Start()
    {
        _ui_heartDefender = GetComponent<UI_HeartDefender>();
        gameManager = FindObjectOfType<GameManager>();
        playerHealth = GameManager.Instance.PlayerHealth;
        playStateMachine = FindObjectOfType<PlayStateMachine>();


        heartDefenderManager = FindObjectOfType<HeartDefenderManager>();

        _originalPosition = transform.position;
        _superDefenderManager = FindObjectOfType<SuperDefenderManager>();

        _ui_heartDefenderInteractions = FindObjectOfType<UI_HeartDefenderInteractions>();

        _ui_playerHand = FindObjectOfType<UI_PlayerHand>();
 
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
        if (isSuperDefender == true) return;

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
        SetSiblingIndex(0);
        _defenderSelected = false;
    }

    // if player tries to purchase a rank upgrade and has enough currency, upgrade the card's rank
    public void PurchaseRankUpgrade()
    {
        if (IsAbleToUpgrade())
        {
            UpgradeCard(GetNextUpgradeCost());

        }
        else
        {
            Debug.Log("You do not have enough money to upgrade this heart or it's at max level");
            SFXManager.Instance.PlayRandomSound(SFXName.PurchaseFailed);
            _ui_heartDefender.ShakeCamera();
        }
    }

    void UpgradeCard(int cost)
    {
        // if player is already in ChooseSuperDefender state and clicks the X icon on defender, transition back to player state and switch back the icon
        if (playStateMachine.GetCurrentState() == PlayState.ChooseSuperDefender)
        {
            playStateMachine.TransitionToPreviousState();
            SwitchUpgradeIcon();
        }
        else
        {
            if (!isSuperDefender)
            {
                if (heartRank == maxLevel)
                {
                    CheckForSuperDefender();
                }
                else
                {
                    UpgradeRank(cost);
                }
            }
        }
    }

    void UpgradeRank(int cost)
    {
        CurrencyManager.RemoveMoney(cost);
        heartRank++;
        // add SubtractDifferenceToPlayer()
        _ui_heartDefender.UpdateRankUI(heartRank);
        //play the sfx 
        SFXManager.Instance.PlaySoundAtIndex(SFXName.LevelUpDefender, heartRank);


        // check if the upgrade icon needs to change to the superdefender icon
        if (heartRank == 10)
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
        isSuperDefender = true; // set this flag to true so player can no longer use to block attacks
        SuperDefender superDefender = gameObject.AddComponent<SuperDefender>();
        heartDefenderManager.RemoveFromDefenderList(this);
        superDefender.InitializeFromCard(card, _defenderSprite);
        _superDefenderManager.AddSuperDefender(superDefender);
        _superDefenderManager.ClearSelectedDefender();
        Destroy(_upgradeIcon_Obj);
        Destroy(_rankCounter_Obj);
        Destroy(this);
        _ui_playerHand.EnablePlayerHandInteractionBtns();

        // if adding this super defender satisfies the superdefender win state, win the game
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
    public void TakeDamage(int damage)
    {
        _ui_heartDefender.ShakeCamera();
        // track damage amt that surpasses defender's rank
        int overDamage = 0;
        _ui_heartDefender.ShowDamage(damage);
        // if damage amount is higher than the defender's, decrease the rank of the heart by the difference between the amount of damage and the heart's rank
        if (damage > TotalHeartRank())
        {
            Debug.Log("Total heart Rank: " + TotalHeartRank());
            overDamage = damage - heartRank;
            Debug.Log("damage: " + damage);
            Debug.Log("overdamage" + overDamage);
            DecreaseRank(overDamage);
            //play the sound for the player taking damage
            SFXManager.Instance.PlayRandomSound(SFXName.DestroyDefender);
        }
        else
        {
            //play the sound for when the defender succesffuly defends
            SFXManager.Instance.PlayRandomSound(SFXName.DefenderDefends);
        }
    }

    void DecreaseRank(int damage)
    {
        // tracks overkill damage
        int overKillDmg = 0;

        overKillDmg = Mathf.Abs(heartRank -= damage);

        _ui_heartDefender.ShowRankDecrease(overKillDmg, heartRank);

        heartRank -= damage;

        // if heartRank is 0 or lower, destroy it and apply overkill damage to player
        if (heartRank <= 0)
        {
            DestroyHeart(overKillDmg);
        }

        // check if need to revert upgrade icon back to arrow icon
        if (heartRank < maxLevel)
        {
            SwitchUpgradeIcon();
        }
    }

    void OverKillDamage(int damage)
    {
        // if this heart dies, player will receive overkill damage
        playerHealth.RemoveHealth(damage);
        SFXManager.Instance.PlayRandomSound(SFXName.PlayerTakeDamage);
    }

    void DestroyHeart(int damage)
    {
        heartDefenderManager.RemoveFromDefenderList(this);
        heartDefenderManager.RemoveDefenderForAttack(this);
        // apply overkill
        OverKillDamage(damage);
        // destroy this heart
        Destroy(this.gameObject);
        SFXManager.Instance.PlayRandomSound(SFXName.DefenderKilled);
    }

    // calculates this heart defender's next upgrade cost
    private int GetNextUpgradeCost()
    {
        int levelUpCost = 0;
        levelUpCost = Mathf.RoundToInt(heartRank * fixedCost);

        return levelUpCost;
    }

    // updates the badge counter

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
            if (heartRank == 10)
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
        int totalHeartRank = heartRank;
        if (_superDefenderManager != null)
        {
            int numberOfKingSuperdefendersInPlay = _superDefenderManager.GetKingDefenders().Count;
            totalHeartRank = heartRank += numberOfKingSuperdefendersInPlay;
        }

        return totalHeartRank;
    }

    // resets the layout of the heart defenders in horizontal layout
    private void SetSiblingIndex(int index) => transform.SetSiblingIndex(index);


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




}
