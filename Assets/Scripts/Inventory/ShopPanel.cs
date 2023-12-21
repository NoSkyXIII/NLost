using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    public event UnityAction<string, int, int> SellPresed;
    private int _cost;
    private int _choosenCount = 1;
    private int _count;
    private Item _item;
    [SerializeField] private ShopPanelUI _panelUI;

    public void Set(Item item, int count)
    {
        _item = item;
        _cost = item.Cost;
        _count = count;
        _panelUI.Set(_item, _count);
        _panelUI.Slider.onValueChanged.AddListener(ValueChanged);
        _panelUI.Sell.onClick.AddListener(Sell);
    }

    private void ValueChanged(float value)
    {
        _choosenCount = (int)value;
        _cost = _item.Cost * _choosenCount;
        _panelUI.UpdateUI(_choosenCount, _cost);
    }

    private void Sell()
    {
        Debug.Log($"Sold: {_choosenCount} on sum {_cost}");
        SellPresed?.Invoke((_item.ID).ToString(), _choosenCount, _cost);
    }
}