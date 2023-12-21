using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "NewInventory", menuName = "Inventory/InventoryModel")]
public class InventoryModel : ScriptableObject
{
    public event Action<Dictionary<int, InventorySlot>> OnInventoryUpdated;
    public IReadOnlyList<InventorySlot> Slots => _slots;
    public int SlotsCount => _slotCount;

    [SerializeField] private int _slotCount = 20;
    [SerializeField] private List<InventorySlot> _slots;

    public void Initialize()
    {
        _slots = new List<InventorySlot>();

        for (int i = 0; i < _slotCount; i++)
        {
            _slots.Add(new InventorySlot());
        }
    }

    public int TryToAddItem(Item item, int count)
    {
        for (int i = 0; i < _slots.Count; i++)
        {
            if (_slots[i].IsEmpty)
                continue;

            if (_slots[i].Item.ID == item.ID)
            {
                int amountPossibleToTake = _slots[i].Item.MaxStack - _slots[i].CurrentCountInSlot;

                if (count > amountPossibleToTake)
                {
                    _slots[i] = _slots[i].ChangeCount(_slots[i].Item.MaxStack);
                    count -= amountPossibleToTake;
                }

                else
                {
                    _slots[i] = _slots[i].ChangeCount(_slots[i].CurrentCountInSlot + count);
                    InformAboutChange();
                    return 0;
                }
            }
        }

        while (count > 0 && IsInventoryFull() == false)
        {
            int newQuantity = Mathf.Clamp(count, 0, item.MaxStack);
            count -= newQuantity;
            AddItemToNewSlot(item, newQuantity);
        }

        InformAboutChange();

        return count;
    }

    public void RemoveItem(string id, int count = 1)
    {
        for (int i = _slots.Count - 1; i >= 0; i--)
        {
            if (_slots[i].IsEmpty)
                continue;

            if (_slots[i].Item.ID == id)
            {
                while (count > 0 && _slots[i].CurrentCountInSlot > 0)
                {
                    _slots[i] = _slots[i].ChangeCount(_slots[i].CurrentCountInSlot - 1);
                    count--;
                }

                if (_slots[i].CurrentCountInSlot == 0)
                    _slots[i] = InventorySlot.GetEmptySlot();
            }
        }

        InformAboutChange();
    }

    private bool IsInventoryFull() => _slots.Where(item => item.IsEmpty).Any() == false;

    private void InformAboutChange()
    {
        OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
    }

    private Dictionary<int, InventorySlot> GetCurrentInventoryState()
    {
        Dictionary<int, InventorySlot> returnValue = new Dictionary<int, InventorySlot>();

        for (int i = 0; i < _slots.Count; i++)
        {
            if (_slots[i].IsEmpty)
                continue;

            returnValue[i] = _slots[i];
        }
        return returnValue;
    }

    private void AddItemToNewSlot(Item item, int count)
    {
        InventorySlot newSlot = new InventorySlot
        {
            Item = item,
            CurrentCountInSlot = count
        };

        for (int i = 0; i < _slots.Count; i++)
        {
            if (_slots[i].IsEmpty)
            {
                _slots[i] = newSlot;
                break;
            }
        }
    }
}