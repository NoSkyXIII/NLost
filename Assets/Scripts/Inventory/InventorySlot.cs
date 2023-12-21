using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class InventorySlot
{
    public int MaxCountInSlot;
    public int CurrentCountInSlot;
    public Item Item;
    public bool IsEmpty => Item == null;
    public bool IsFull => CurrentCountInSlot == MaxCountInSlot;

    public InventorySlot ChangeCount(int newCount)
    {
        return new InventorySlot
        {
            Item = this.Item,
            CurrentCountInSlot = newCount
        };
    }

    public static InventorySlot GetEmptySlot() => new InventorySlot
    {
        Item = null,
        CurrentCountInSlot = 0
    };
}