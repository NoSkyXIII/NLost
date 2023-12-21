using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private InventoryModel _model;
    [SerializeField] private InventoryView _view;
    [SerializeField] private Shop _shop;
    [SerializeField] private MoneyHolder _moneyHolder;

    public void ShowInventory()
    {
        _view.InventoryShower();
    }

    public int TakeItems(Item item, int count)
    {
        return _model.TryToAddItem(item, count);
    }

    private void Start()
    {
        _model.Initialize();
        _view.Initialize(_model.SlotsCount);
        _model.OnInventoryUpdated += OnInventoryChanged;
        _shop.SellItem += SellItem;
    }

    private void OnInventoryChanged(Dictionary<int, InventorySlot> slots)
    {
        _view.ResetAllItems();

        foreach(var slot in slots)
        {
            _view.UpdateItem(slot.Key, slot.Value.Item.Sprite, slot.Value.CurrentCountInSlot);
        }

        _shop.UpdateDictionary(slots);
    }

    private void SellItem(string index, int count, int cost)
    {
        _model.RemoveItem(index, count);
        _moneyHolder.AddMoney(cost);
    }
}