using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationFocus : MonoBehaviour
{
    public void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus == false)
            OnFocusLost();
        else
            OnFocusReturn();
    }

    private void OnFocusLost()
    {
        SaveSystem.Instance.SaveAll();
        Time.timeScale = 0;
        AudioManager.Instance.PauseAll();
    }

    private void OnFocusReturn()
    {
        Time.timeScale = 1;
        AudioManager.Instance.ResetAll();
    }
}