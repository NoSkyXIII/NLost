using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Item")]
public class Item : ScriptableObject
{
    public string ID => _id;
    public Sprite Sprite => _sprite;
    public int MaxStack => _maxStack;
    public int Cost => _cost;

    [SerializeField] private int _maxStack;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _cost;

    [SerializeField] private string _id;
    
    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        _id = System.Guid. NewGuid().ToString();
    }
}