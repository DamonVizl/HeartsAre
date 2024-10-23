using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Enemy : MonoBehaviour
{

    public GameObject enemyObj;
    public void ShowEnemy()
    {
        enemyObj.SetActive(true);
    }

    public void HideEnemy()
    {
        enemyObj.SetActive(false);
    }
}
