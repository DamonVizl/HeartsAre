using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SuperDefenderManager : MonoBehaviour
{
    private List<SuperDefender> _jackDefenders = new List<SuperDefender>();
    private List<SuperDefender> _queenDefenders = new List<SuperDefender>();
    private List<SuperDefender> _kingDefenders = new List<SuperDefender>();

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
    }

    // Get lists for each type
    public List<SuperDefender> GetJackDefenders() => _jackDefenders;
    public List<SuperDefender> GetQueenDefenders() => _queenDefenders;
    public List<SuperDefender> GetKingDefenders() => _kingDefenders;
}
