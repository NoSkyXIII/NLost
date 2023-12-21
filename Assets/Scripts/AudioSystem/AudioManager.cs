using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource _musicSource, _effectSource;

    private void Awake()
    {
        if(Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void PlaySound(AudioClip audioClip)
    {
        _effectSource.PlayOneShot(audioClip);
    }

    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }

    public void ToggleEffect()
    {
        _effectSource.mute = !_effectSource.mute;
    }

    public void ToggleMusic()
    {
        _musicSource.mute = !_musicSource.mute;
    }

    public void PauseAll()
    {
        _effectSource.Pause();
        _musicSource.Pause();
    }

    public void ResetAll()
    {
        _effectSource.Play();
        _musicSource.Play();
    }
}
