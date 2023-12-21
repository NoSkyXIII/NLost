using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandUI : MonoBehaviour
{
    [SerializeField] private IslandCellUI _cellPrefab;
    [SerializeField] private Island _locker;
    [SerializeField] private RectTransform _canvasRect;
    [SerializeField] private RectTransform _rectPanel;
    [SerializeField] private List<IslandCellUI> _cells;

    private void Start()
    {
        Init();
        _locker.CountChanges += UpdateUI;
    }

    public void Init()
    {
        for (int i = 0; i < _locker.RequiredItems.Count; i++)
        {
            IslandCellUI cell = Instantiate(_cellPrefab, _canvasRect.transform.position, _canvasRect.transform.rotation, _rectPanel);

            cell.Init(_locker.RequiredItems[i].Sprite, _locker.RequiredItemsCount[i]);

            _cells.Add(cell);
        }
    }

    private void UpdateUI()
    {
        for (int i = 0; i < _cells.Count; i++)
        {
            if(_locker.RequiredItemsCount[i] <= 0)
                _cells[i].gameObject.SetActive(false);

            _cells[i].Set(_locker.RequiredItemsCount[i]);
        }
    }
}