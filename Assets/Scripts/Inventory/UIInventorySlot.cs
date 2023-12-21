using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private Image _itemImage;
    [SerializeField] private TMP_Text _countText;

    public void ResetData()
    {
        _background.gameObject.SetActive(false);
    }

    public void SetData(Sprite sprite, int quantity)
    {
        _background.gameObject.SetActive(true);
        _itemImage.gameObject.SetActive(true);
        _itemImage.sprite = sprite;
        _countText.text = quantity.ToString();
    }
}