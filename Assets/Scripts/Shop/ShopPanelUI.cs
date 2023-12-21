using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ShopPanelUI : MonoBehaviour
{
    public Slider Slider => _slider;
    public Button Sell => _sell;
    [SerializeField] private Slider _slider;
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _count;
    [SerializeField] private TMP_Text _cost;
    [SerializeField] private Button _sell;


    public void Set(Item item, int count)
    {
        _icon.sprite = item.Sprite;
        Slider.minValue = 1;
        Slider.maxValue = count;
        UpdateUI((int)_slider.value, item.Cost);
    }

    public void UpdateUI(int count, int cost)
    {
        _count.text = count.ToString();
        _cost.text = cost.ToString();
    }
}