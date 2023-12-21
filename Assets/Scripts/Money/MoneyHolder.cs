using UnityEngine;

public class MoneyHolder : MonoBehaviour, IDataHandler
{
    public int Money => _currentMoney;

    [SerializeField] private MoneyUI _moneyUI;

    [SerializeField] private int _currentMoney = 0;

    public void AddMoney(int money)
    {
        _currentMoney += money;

        _moneyUI.SetMoneyUI(_currentMoney);

        SaveSystem.Instance.SaveAll();
        LeaderBoard.Instance.SetResult(_currentMoney);
    }

    public void Save(GameData data)
    {
        data.Money = _currentMoney;
    }

    public void Load(GameData data, bool gameStatus = false)
    {
        if (!gameStatus)
        {
            _currentMoney = data.Money;
            _moneyUI.SetMoneyUI(_currentMoney);
        }
    }
}