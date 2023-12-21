using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public event UnityAction<string, int, int>  SellItem;

    [SerializeField] private ShopUI _shopUI;
    private Dictionary<Item, int> _shopDictionary = new Dictionary<Item, int>();
    [SerializeField] private ShopPanel _panelPrefab;

    public void UpdateDictionary(Dictionary<int, InventorySlot> inventory)
    {
        _shopDictionary.Clear();
        _shopUI.Reset();

        foreach (var slot in inventory)
        {
            if (_shopDictionary.ContainsKey(slot.Value.Item))
                _shopDictionary[slot.Value.Item] += slot.Value.CurrentCountInSlot;
            
            else
                _shopDictionary.Add(slot.Value.Item, slot.Value.CurrentCountInSlot);
        }

        foreach (var item in _shopDictionary)
        {
            AddPanel(item.Key, item.Value);
        }
    }

    private void AddPanel(Item item, int count)
    {
        ShopPanel panel = Instantiate(_panelPrefab, Vector3.zero, Quaternion.identity);

        _shopUI.AddPanel(panel);
        panel.Set(item, count);
        panel.SellPresed += Sold;
    }

    private void Sold(string id, int count, int cost)
    {
        SellItem?.Invoke(id, count, cost);
    }
}