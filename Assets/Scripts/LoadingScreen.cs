using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private float _rotationSpeed = -1f;

    private void Update()
    {
        _image.transform.Rotate(0f, 0f, _rotationSpeed);
    }
}