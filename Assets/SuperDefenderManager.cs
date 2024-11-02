using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SuperDefenderManager : MonoBehaviour
{
    private List<SuperDefender> _jackDefenders = new List<SuperDefender>();
    private List<SuperDefender> _queenDefenders = new List<SuperDefender>();
    private List<SuperDefender> _kingDefenders = new List<SuperDefender>();

    public Sprite _jackSuperDefenderSprite;
    public Sprite _queenSuperDefenderSprite;
    public Sprite _kingSuperDefenderSprite;

    private HeartDefender _selectedHeartDefender;
    private HeartDefenderManager _heartDefenderManager;

    // Dictionary to map SuperDefenderType to their corresponding sprites
    private Dictionary<SuperDefender.SuperDefenderType, Sprite> _superDefenderSprites;

    public List<SuperDefender> ActiveSuperDefenders { get; private set; } = new List<SuperDefender>();

    private void Start()
    {
        _heartDefenderManager = FindObjectOfType<HeartDefenderManager>();

        // Initialize the dictionary with sprites for each SuperDefenderType
        _superDefenderSprites = new Dictionary<SuperDefender.SuperDefenderType, Sprite>
        {
            { SuperDefender.SuperDefenderType.Jack, _jackSuperDefenderSprite },
            { SuperDefender.SuperDefenderType.Queen, _queenSuperDefenderSprite },
            { SuperDefender.SuperDefenderType.King, _kingSuperDefenderSprite }
        };
    }

    public void AddSuperDefender(SuperDefender defender)
    {
        // Add defender to the appropriate list
        switch (defender.superDefenderType)
        {
            case SuperDefender.SuperDefenderType.Jack:
                _jackDefenders.Add(defender);
                break;
            case SuperDefender.SuperDefenderType.Queen:
                _queenDefenders.Add(defender);
                break;
            case SuperDefender.SuperDefenderType.King:
                _kingDefenders.Add(defender);
                break;
        }

        // Add to active list and update UI
        ActiveSuperDefenders.Add(defender);
        FindObjectOfType<SuperDefenderUI>().UpdateSuperDefenderIcons(ActiveSuperDefenders);
    }


    // Method to get the sprite based on SuperDefenderType
    public Sprite GetSpriteForDefenderType(SuperDefender.SuperDefenderType type)
    {
        return _superDefenderSprites.TryGetValue(type, out var sprite) ? sprite : null;
    }

    // Get lists for each type
    public List<SuperDefender> GetJackDefenders() => _jackDefenders;
    public List<SuperDefender> GetQueenDefenders() => _queenDefenders;
    public List<SuperDefender> GetKingDefenders() => _kingDefenders;

    public void SetSelectedHeartDefender(HeartDefender defender)
    {
        _selectedHeartDefender = defender;
    }

    public void ClearSelectedDefender()
    {
        _selectedHeartDefender = null;
    }

    public HeartDefender GetSelectedDefender()
    {
        return _selectedHeartDefender;
    }

    public bool SuperDefenderWinStateMet()
    {
        bool allThreeInPlay = _jackDefenders.Count > 0 && _queenDefenders.Count > 0 && _kingDefenders.Count > 0;
        bool threeOfSameInPlay = _jackDefenders.Count == 3 || _queenDefenders.Count == 3 || _kingDefenders.Count == 3;

        return allThreeInPlay || threeOfSameInPlay;
    }

    public HeartDefenderManager GetHeartDefenderManager()
    {
        return _heartDefenderManager;
    }

}
