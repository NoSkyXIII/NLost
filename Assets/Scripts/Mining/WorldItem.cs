using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItem : MonoBehaviour
{
    [field: SerializeField]
    public Item ItemSO { get; private set; }
    public int Count { get; set; } = 1; 

    public Item Destroy()
    {
        Destroy(gameObject);
        return ItemSO;
    }

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        transform.Rotate(0, 0.1f, 0);
    }
}
