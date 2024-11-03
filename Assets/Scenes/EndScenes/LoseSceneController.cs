using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseSceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TransferToStartScene());
    }

    private IEnumerator TransferToStartScene()
    {
        yield return new WaitForSeconds(4);
        SceneController.Instance.SetScene(0); 
    }
}
