using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ConveyorSaver : MonoBehaviour, IDataHandler
{
    [SerializeField] private List<Conveyor> _conveyors;

    public void Load(GameData data, bool isNewGame = false)
    {
        if (!isNewGame)
        {
            for (int i = 0; i < _conveyors.Count; i++)
            {
                _conveyors[i].Load(data.ConveyorValue[i]);
            }
        }
    }

    public void Save(GameData data)
    {
        for (int i = 0; i < _conveyors.Count; i++)
        {
            List<int> list = new List<int>();

            for (int j = 0; j < _conveyors[i].LoadedItems.Count; j++)
            {
                list.Add(_conveyors[i].LoadedItems[j]);
            }

            data.ConveyorValue.Add(list);
        }
    }
}