using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Island : MonoBehaviour, ITakeble
{
    public bool Status => _status;
    public IReadOnlyList<Item> RequiredItems => _requiredItems;
    public IReadOnlyList<int> RequiredItemsCount => _requiredItemsCount;

    public event UnityAction CountChanges;

    [Header("Object to Unlock")]
    [SerializeField] private GameObject _invisiableWall;
    [SerializeField] private Island _oppeningIsland;
    [SerializeField] private GameObject _locker;

    [Header("Items Setting")]
    [SerializeField] private List<Item> _requiredItems = new List<Item>();
    [SerializeField] private List<int> _requiredItemsCount = new List<int>();

    [Header("Other Setting")]
    [SerializeField] private AudioClip _clip;
    [SerializeField] private bool _status = false;

    private Coroutine _takeCoroutine;

    public void Take(InventoryModel inventoryModel)
    {
        _takeCoroutine = StartCoroutine(TakeCoroutine(inventoryModel));
    }

    public void StopTake()
    {
        if (_takeCoroutine != null)
        {
            StopCoroutine(_takeCoroutine);
        }    
    }
    public void SetActive()
    {
        _status = true;
    }

    private IEnumerator TakeCoroutine(InventoryModel inventoryModel)
    {
        for (int i = 0; i < _requiredItems.Count; i++)
        {
            for (int y = 0; y < inventoryModel.SlotsCount; y++)
            {
                if(!inventoryModel.Slots[y].IsEmpty && inventoryModel.Slots[y].Item.ID == _requiredItems[i].ID)
                {
                    while (inventoryModel.Slots[y].CurrentCountInSlot > 0 && _requiredItemsCount[i] > 0)
                    {
                        inventoryModel.RemoveItem(inventoryModel.Slots[y].Item.ID);
                        _requiredItemsCount[i]--;
                        AudioManager.Instance.PlaySound(_clip);
                        CountChanges?.Invoke();

                        yield return new WaitForSeconds(0.1f);
                    }
                }
            }
        }

        CheckState();
    }

    private void CheckState()
    {
        int totalRequiredItemsCount = 0;

        for (int i = 0; i < _requiredItems.Count; i++)
        {
            totalRequiredItemsCount += _requiredItemsCount[i];
        }

        if(totalRequiredItemsCount <= 0) 
        {
            if(_oppeningIsland != null && !_oppeningIsland.gameObject.activeSelf)
                _oppeningIsland.gameObject.SetActive(true);

            if(_invisiableWall != null)
                 _invisiableWall.SetActive(false);

            _locker.SetActive(false);

            _oppeningIsland.SetActive();
        }
    }

    public void Set(List<int> data)
    {
        for (int i = 0; i < _requiredItems.Count; i++)
        {
            _requiredItemsCount[i] = data[i];
        }

        CountChanges?.Invoke();
        CheckState();
    }
}