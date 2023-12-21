using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class IslandSaver : MonoBehaviour, IDataHandler
{
    [SerializeField] private List<Island> _islands;

    public void Load(GameData data, bool gameStatus = false)
    {
        if (gameStatus)
        {
            for (int i = 1; i < _islands.Count; i++)
            {
                _islands[i].gameObject.SetActive(false);
            }
        }

        else
        {
            for (int i = 0; i < _islands.Count; i++)
            {
                _islands[i].Set(data.LockerValue[i]);
            }

            foreach (var island in _islands)
            {
                island.gameObject.SetActive(false);

                if (island.Status)
                    island.gameObject.SetActive(true);
            }
        }
    }

    public void Save(GameData data)
    {
        for (int i = 0; i < _islands.Count; i++)
        {
            data.Status.Add(_islands[i].gameObject.activeSelf);

            List<int> list = new List<int>();

            for (int y = 0; y < _islands[i].RequiredItems.Count; y++)
            {
                list.Add(_islands[i].RequiredItemsCount[y]);
            }

            data.LockerValue.Add(list);
        }
    }
}