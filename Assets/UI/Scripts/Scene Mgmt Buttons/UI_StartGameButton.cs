using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_StartGameButton : MonoBehaviour
{
    public void StartGame()
    {
        SceneController.Instance.SetScene(1); 
    }
}
