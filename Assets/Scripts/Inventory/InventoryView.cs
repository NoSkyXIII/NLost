using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class InventoryView : MonoBehaviour
{
    [SerializeField] private Transform _showPosition;
    [SerializeField] private Transform _hidePosition;
    [SerializeField] private float _showTime;
    [SerializeField] private InventorySlotUI _slotPrefab;
    [SerializeField] private RectTransform _contentPanel;

    private List<InventorySlotUI> _uiSlots = new List<InventorySlotUI>();

    private bool _isEnabled = false;

    public void Initialize(int inventorysize)
    {
        for (int i = 0; i < inventorysize; i++)
        {
            InventorySlotUI uiItem = Instantiate(_slotPrefab, Vector3.zero, Quaternion.identity);

            uiItem.transform.SetParent(_contentPanel);

            uiItem.transform.localScale = new Vector3(1, 1, 1);

            _uiSlots.Add(uiItem);
            _uiSlots[i].ResetData();
        }
    }

    public void ResetAllItems()
    {
        foreach (var item in _uiSlots)
        {
            item.ResetData();
        }
    }

    public void UpdateItem(int index, Sprite sprite, int count)
    {
        if (_uiSlots.Count > index)
        {
            _uiSlots[index].ResetData();
            _uiSlots[index].SetData(sprite, count);
        }
    }

    public void InventoryShower()
    {
        if (_isEnabled == false)
        {
            gameObject.transform.DOMove(_showPosition.position, _showTime, true);
            gameObject.SetActive(true);
            _isEnabled = true;
        }
        
        else
        {
            gameObject.transform.DOMove(_hidePosition.position, _showTime, true);

            StartCoroutine(Hide());
        }
    }
    
    private IEnumerator Hide()
    {
        yield return new WaitForSeconds(_showTime);

        gameObject.SetActive(false);
        _isEnabled = false;
        
        StopCoroutine(Hide());
    }
}
