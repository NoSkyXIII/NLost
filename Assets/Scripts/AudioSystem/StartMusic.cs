using UnityEngine;

public class StartMusic : MonoBehaviour
{
    [SerializeField] private AudioClip _musicClip;

    void Start()
    {
        AudioManager.Instance.PlaySound( _musicClip );
    }
}
