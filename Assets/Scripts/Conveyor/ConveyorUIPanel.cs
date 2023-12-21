using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConveyorUIPanel : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _loadedCount;
    [SerializeField] private TMP_Text _requiredCount;

    public void Init(Sprite sprite, int count, int requiredCount)
    {
        _image.sprite = sprite;
        _loadedCount.text = count.ToString();
        _requiredCount.text = requiredCount.ToString();
    }

    public void Set(int count)
    {
        _loadedCount.text = count.ToString();
    }
}
