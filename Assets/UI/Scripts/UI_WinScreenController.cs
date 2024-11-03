using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_WinScreenController : MonoBehaviour
{
    public void ReturnToStartScene()
    {
        SceneController.Instance.SetScene(0); 
    }
}
