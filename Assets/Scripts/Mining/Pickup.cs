using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Pickup : MonoBehaviour
{
    [SerializeField] private InventoryModel _inventoryModel;
    [SerializeField] private AudioClip _audioClip;

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.TryGetComponent<WorldItem>(out WorldItem worldItem))
        {
            if (worldItem != null)
            {
                TryAddItemToInventory(worldItem);
            }
        }
    }

    private void TryAddItemToInventory(WorldItem worldItem)
    {
        int reminder = _inventoryModel.TryToAddItem(worldItem.ItemSO, worldItem.Count);

        if (reminder == 0)
        {
            DestroyWorldItem(worldItem);
            AudioManager.Instance.PlaySound(_audioClip);
        }
        else
        {
            worldItem.Count = reminder;
        }
    }

    private void DestroyWorldItem(WorldItem worldItem)
    {
        worldItem.Destroy();
    }
}