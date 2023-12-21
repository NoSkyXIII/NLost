using UnityEngine;

public class ToggleJoystick : MonoBehaviour
{
    [SerializeField] private GameObject _joystick;

    private bool _isActive;

    private void Start()
    {
        _isActive = _joystick.activeSelf;
    }

    public void Toggle()
    {
        if(_isActive)
        {
            _joystick.SetActive(false);
            _isActive = false;
        }

        else
        {
            _joystick.SetActive(true);
            _isActive = true;
        }
    }
}
