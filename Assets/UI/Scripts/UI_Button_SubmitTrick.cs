using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Button_SubmitTrick : MonoBehaviour
{
    public static Action OnSubmitButtonPressed;
    public void SubmitTrick()
    {
        OnSubmitButtonPressed?.Invoke();
    }
}
