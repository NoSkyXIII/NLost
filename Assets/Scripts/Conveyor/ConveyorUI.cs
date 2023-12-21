using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConveyorUI : MonoBehaviour
{
    [SerializeField] private Image _itemProduceImage;
    [SerializeField] private ConveyorUIPanel _panelPrefab;
    [SerializeField] private Conveyor _conveyor;
    [SerializeField] private RectTransform _canvasRect;
    [SerializeField] private RectTransform _rectPanel;
    [SerializeField] private List<ConveyorUIPanel> _cells;

    private void Start()
    {
        Init();
        _itemProduceImage.sprite = _conveyor.ProducedItem.Sprite;
        _conveyor.CountChanges += UpdateUI;
    }

    public void Init()
    {
        for (int i = 0; i < _conveyor.RequiredItems.Count; i++)
        {
            ConveyorUIPanel panel = Instantiate(_panelPrefab, _canvasRect.transform.position, _canvasRect.transform.rotation, _rectPanel);

            panel.Init(_conveyor.RequiredItems[i].Sprite, _conveyor.LoadedItems[i], _conveyor.RequiredItemsCount[i]);

            _cells.Add(panel);
        }
    }

    private void UpdateUI()
    {
        for (int i = 0; i < _cells.Count; i++)
        {
            _cells[i].Set(_conveyor.LoadedItems[i]);
        }
    }
}
