using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyUI;

    public void SetMoneyUI(int money)
    {
        _moneyUI.text = money.ToString();
    }
}
