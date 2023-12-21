using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IslandCellUI : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _countText;

    public void Init(Sprite sprite, int count)
    {
        _image.sprite = sprite;
        _countText.text = count.ToString();
    }

    public void Set(int count)
    {
        _countText.text = count.ToString();
    }
}
