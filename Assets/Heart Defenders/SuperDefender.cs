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

    public void Initialize(SuperDefenderType type, Image _defenderImageRef)
    {
        _superDefenderManager = FindObjectOfType<SuperDefenderManager>();
        this.superDefenderType = type;
        ChangeDefenderSprite(_defenderImageRef);
        AssignAbility(type);
        superDefenderAbility?.Activate();
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

    public void ApplyPassiveEffect()
    {
        superDefenderAbility?.ApplyPassiveEffect();
    }

    public SuperDefenderType GetRandomSuperDefenderType()
    {
        SuperDefenderType[] types = { SuperDefenderType.Jack, SuperDefenderType.Queen, SuperDefenderType.King };
        int randomIndex = UnityEngine.Random.Range(0, types.Length);
        return types[randomIndex];
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
