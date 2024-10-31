using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuperDefender : MonoBehaviour
{
    public enum SuperDefenderType
    { 
        Jack,
        Queen,
        King
    }

    public SuperDefenderType superDefenderType;
    private ISuperDefenderAbility superDefenderAbility;
    private SuperDefenderManager _superDefenderManager;
    private HeartDefenderManager _heartDefenderManager;

    void Initialize(SuperDefenderType type, Image _defenderImageRef)
    {
        _superDefenderManager = FindObjectOfType<SuperDefenderManager>();
        _heartDefenderManager = FindObjectOfType<HeartDefenderManager>();
        ChangeDefenderSprite(_defenderImageRef);
        AssignAbility(type);
        superDefenderAbility?.Activate(_superDefenderManager);
    }

    public void InitializeFromCard(Card card, Image _defenderImageRef)
    {

        superDefenderType = MapCardValueToDefenderType(card.GetCardValueAsInt());
        Initialize(superDefenderType, _defenderImageRef);
    }

    private void AssignAbility(SuperDefenderType type)
    {
        switch (type)
        {
            case SuperDefenderType.Jack:
                superDefenderAbility = new JackDefenderAbility();
                break;
            case SuperDefenderType.Queen:
                superDefenderAbility = new QueenDefenderAbility();
                break;
            case SuperDefenderType.King:
                superDefenderAbility = new KingDefenderAbility();
                break;
        }
    }

    private SuperDefenderType MapCardValueToDefenderType(int cardValue)
    {
        switch (cardValue)
        {
            case (int)Value.Jack:
                return SuperDefenderType.Jack;
            case (int)Value.Queen:
                return SuperDefenderType.Queen;
            case (int)Value.King:
                return SuperDefenderType.King;
            default:
                throw new System.ArgumentException("card is not a face card. Can't be used for a super defender upgrade.");
        }
    }

    public void ApplyPassiveEffect()
    {
        superDefenderAbility?.ApplyPassiveEffect();
    }

    public void ChangeDefenderSprite(Image _defenderImageRef)
    {
        if (_superDefenderManager == null) return;

        // Set the sprite based on the defender type
        switch (superDefenderType)
        {
            case SuperDefenderType.Jack:
                _defenderImageRef.sprite = _superDefenderManager._jackSuperDefenderSprite;
                break;
            case SuperDefenderType.Queen:
                _defenderImageRef.sprite = _superDefenderManager._queenSuperDefenderSprite;
                break;
            case SuperDefenderType.King:
                _defenderImageRef.sprite = _superDefenderManager._kingSuperDefenderSprite;
                break;
        }
    }


}
