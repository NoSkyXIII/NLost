using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private void Start()
    {
        AudioManager.Instance.ChangeMasterVolume(_slider.value);
        _slider.onValueChanged.AddListener(value => AudioManager.Instance.ChangeMasterVolume(value));
    }
}
