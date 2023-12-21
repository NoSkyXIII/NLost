using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStorage : MonoBehaviour
{
    public static ItemStorage Instance;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    [SerializeField] private List<string> _id;
    [SerializeField] private List<Item> _items;

    public Item GetItem(string id){

        int index = _id.IndexOf(id);

//        Debug.Log(index);

        return _items[index];
    }
}
