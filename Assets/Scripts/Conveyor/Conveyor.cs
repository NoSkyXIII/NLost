using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Conveyor : MonoBehaviour, ITakeble
{
    public IReadOnlyList<Item> RequiredItems => _requiredItems;
    public IReadOnlyList<int> RequiredItemsCount => _requiredItemsCount;
    public IReadOnlyList<int> LoadedItems => _loadedItems;
    public Item ProducedItem => _producedItem;

    public event UnityAction CountChanges;

    [SerializeField] private Item _producedItem;
    [SerializeField] private float _delay;
    [SerializeField] private AudioClip[] _clip;
    [SerializeField] private WorldItem _itemBlock;
    [SerializeField] private Transform _spawnPoint;

    [Header("Items Setting")]
    [SerializeField] private List<Item> _requiredItems = new List<Item>();
    [SerializeField] private List<int> _requiredItemsCount = new List<int>();
    [SerializeField] private List<int> _loadedItems = new List<int>();
    [SerializeField] private List<int> _maxLoadedItems = new List<int>();

    private Coroutine _takeCoroutine;
    private bool _isWork = false;

    public void Load(List<int> values)
    {
        for (int i = 0; i < _requiredItems.Count; i++)
        {
            _loadedItems[i] = values[i];
        }

        CountChanges?.Invoke();
    }

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

    private IEnumerator TakeCoroutine(InventoryModel inventoryModel)
    {
        for (int i = 0; i < _requiredItems.Count; i++)
        {
            for (int y = 0; y < inventoryModel.SlotsCount; y++)
            {
                if (!inventoryModel.Slots[y].IsEmpty && inventoryModel.Slots[y].Item.ID == _requiredItems[i].ID)
                {
                    while (inventoryModel.Slots[y].CurrentCountInSlot > 0 && _requiredItemsCount[i] > 0 && _loadedItems[i] < _maxLoadedItems[i])
                    {
                        inventoryModel.RemoveItem(inventoryModel.Slots[y].Item.ID);

                        _loadedItems[i]++;
                        AudioManager.Instance.PlaySound(_clip[0]);
                        CountChanges?.Invoke();

                        if (!_isWork)
                            CheckState();

                        yield return new WaitForSeconds(0.1f);
                    }
                }
            }
        }

        if (!_isWork)
            CheckState();
    }

    private void CheckState()
    {
        int check = 0;

        for (int i = 0; i < _requiredItems.Count; i++)
        {
            if (_loadedItems[i] >= _requiredItemsCount[i])
                check++;
        }

        if (check == _requiredItems.Count)
        {
            _isWork = true;

            StartCoroutine(Produce());
        }
        else
        {
            StopCoroutine(Produce());

            _isWork = false;
        }
    }

    private IEnumerator Produce()
    {
        yield return new WaitForSeconds(_delay);

        for (int i = 0; i < _requiredItems.Count; i++)
        {
            _loadedItems[i] -= _requiredItemsCount[i];
        }

        AudioManager.Instance.PlaySound(_clip[1]);

        DropResource();

        CountChanges?.Invoke();

        CheckState();
    }

    private void DropResource()
    {
        WorldItem miniblock = Instantiate(_itemBlock, _spawnPoint.position, Quaternion.identity);
        miniblock.GetComponent<Rigidbody>().AddForce(Vector3.forward, ForceMode.Impulse);
    }
}