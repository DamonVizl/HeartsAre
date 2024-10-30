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


    public List<SuperDefender> ActiveSuperDefenders { get; private set; } = new List<SuperDefender>();


    private void Start()
    {
        _heartDefenderManager = FindObjectOfType<HeartDefenderManager>();
    }

    public void AddSuperDefender(SuperDefender defender)
    {
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

        ActiveSuperDefenders.Add(defender);
    }

    public void RemoveSuperDefender(SuperDefender defender)
    {
        switch (defender.superDefenderType)
        {
            case SuperDefender.SuperDefenderType.Jack:
                _jackDefenders.Remove(defender);
                break;
            case SuperDefender.SuperDefenderType.Queen:
                _queenDefenders.Remove(defender);
                break;
            case SuperDefender.SuperDefenderType.King:
                _kingDefenders.Remove(defender);
                break;
        }

        ActiveSuperDefenders.Remove(defender);
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
