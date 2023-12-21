using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public int Money;
    public List<bool> Status;
    public Dictionary<string, int> InventoryData;
    public List<List<int>> LockerValue;
    public List<List<int>> ConveyorValue;

    public GameData()
    {
        Money = 0;

        InventoryData = new Dictionary<string, int>();

        Status = new List<bool>();

        LockerValue = new List<List<int>>();

        ConveyorValue = new List<List<int>>();
    }
}