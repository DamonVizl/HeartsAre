using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseMenu : MonoBehaviour
{

    public GameObject menuObj;

    public void CloseThisMenu()
    {
        menuObj.SetActive(false);
    }
}
