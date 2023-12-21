using System;
using UnityEngine;

[Serializable]
public class InventorySaver : MonoBehaviour, IDataHandler
{
    [SerializeField] private InventoryModel _inventoryModel;

    public void Save(GameData data)
    {
        for (int i = 0; i < _inventoryModel.Slots.Count; i++)
        {
            if(_inventoryModel.Slots[i].Item != null)
            {
                if(data.InventoryData.ContainsKey(_inventoryModel.Slots[i].Item.ID))
                    data.InventoryData[_inventoryModel.Slots[i].Item.ID] += _inventoryModel.Slots[i].CurrentCountInSlot;
                
                else
                    data.InventoryData.Add(_inventoryModel.Slots[i].Item.ID, _inventoryModel.Slots[i].CurrentCountInSlot);
            }
        }
    }

    public void Load(GameData data, bool isNewGame = false)
    {
        if (isNewGame)
            return;

        foreach (var item in data.InventoryData)
        {
            Item itemToAdd = ItemStorage.Instance.GetItem(item.Key);

            if(itemToAdd != null)
            {
                _inventoryModel.TryToAddItem(itemToAdd, item.Value);
            }
        }
    }
}