using UnityEngine;

public class ToggleAudio : MonoBehaviour
{
    [SerializeField] private bool _toggleEffect, _toggleMusic;

    public void Toggle()
    {
        if(_toggleEffect)
            AudioManager.Instance.ToggleEffect();

        if(_toggleMusic)
            AudioManager.Instance.ToggleMusic();
    }
}