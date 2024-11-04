using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseSceneController : MonoBehaviour
{
    [SerializeField] Button _quitButton;
    [SerializeField] CanvasGroup _canvasGroup;
    [SerializeField] float _buttonWaitTime = 3f; 

    public void QuitGameButton()
    {
        Application.Quit();
    }
    // Start is called before the first frame update
    void Start()
    {
        _quitButton.enabled = false;
        _canvasGroup.alpha = 0; 
        StartCoroutine(ShowQuitButton());
    }
    private IEnumerator ShowQuitButton()
    {
        yield return new WaitForSeconds(_buttonWaitTime);
        _quitButton.enabled = true;
        _canvasGroup.alpha = 1; 
    }
    private IEnumerator TransferToStartScene()
    {
        yield return new WaitForSeconds(4);
        SceneController.Instance.SetScene(0); 
    }
}
