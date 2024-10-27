using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_MessageManager : MonoBehaviour
{
    public static UI_MessageManager Instance;
    private Image _image; 
    [SerializeField] private TextMeshProUGUI _messageText;
    [SerializeField] private float _messageShowTime = 2.0f;  
    
    private void OnEnable()
    {
        if (Instance == null)
        {
            Instance = this;
            _image = GetComponent<Image>();
            TrickScorer.OnTrickScored += ShowMessage; 
        }
        else Destroy(this);
    }
    public void ShowMessage(string message)
    {
        StopAllCoroutines();
        _messageText.text = message;
        _messageText.enabled = true;
        _image.enabled = true;
        StartCoroutine(HideMessage());
    }
    private IEnumerator HideMessage()
    {
        yield return new WaitForSeconds(_messageShowTime);
        _image.enabled = false;
        _messageText.enabled = false;
    }
}
