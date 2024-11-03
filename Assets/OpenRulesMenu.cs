using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenRulesMenu : MonoBehaviour
{
    public GameObject menuObj;

    public void OpenThisMenu()
    {
        menuObj.SetActive(true);
    }
}
