using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private RectTransform _contentPanel;
    [SerializeField] private List<ShopPanel> _shopPanels;

    public void AddPanel(ShopPanel panel)
    {
        panel.transform.SetParent(_contentPanel);

        panel.transform.localScale = new Vector3(1, 1, 1);

        _shopPanels.Add(panel);
    }

    public void Reset()
    {
        foreach(Transform child in _contentPanel)
        {
            Destroy(child.gameObject);
        }

        _shopPanels.Clear();
    }

    private void OnEnable()
    {
        transform.DOScale(1, 0.2f);
    }

    private void OnDisable()
    {
        transform.DOScale(0, 0.2f);
    }
}
