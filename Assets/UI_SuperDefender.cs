using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuperDefenderUI : MonoBehaviour
{
    public Image[] superDefenderSlots; // Assign slots in the inspector
    public Sprite jackIcon;
    public Sprite queenIcon;
    public Sprite kingIcon;

    private Dictionary<SuperDefender.SuperDefenderType, Sprite> defenderIconMap;

    private void Start()
    {
        // Initialize the dictionary to map defender types to icons
        defenderIconMap = new Dictionary<SuperDefender.SuperDefenderType, Sprite>
        {
            { SuperDefender.SuperDefenderType.Jack, jackIcon },
            { SuperDefender.SuperDefenderType.Queen, queenIcon },
            { SuperDefender.SuperDefenderType.King, kingIcon }
        };
    }

    // Call this method when a new SuperDefender is unlocked
    public void UpdateSuperDefenderIcons(List<SuperDefender> activeSuperDefenders)
    {
        // Clear existing icons
        foreach (Image slot in superDefenderSlots)
        {
            slot.sprite = null;
            slot.enabled = false; // Hide the icon if there's no defender
        }

        // Populate icons based on active SuperDefenders
        for (int i = 0; i < activeSuperDefenders.Count && i < superDefenderSlots.Length; i++)
        {
            SuperDefender defender = activeSuperDefenders[i];
            if (defenderIconMap.TryGetValue(defender.superDefenderType, out Sprite icon))
            {
                superDefenderSlots[i].sprite = icon;
                superDefenderSlots[i].enabled = true; // Show the icon
            }
        }
    }
}
