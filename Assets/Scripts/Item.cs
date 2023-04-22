using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItem",menuName = "ScriptableObject/InventoryItem", order = 61)]
public class Item : ScriptableObject
{
    public TypeItem TypeItem;
    public string Name = "Item";
    public Sprite Icon;
    [Range(1,5f)]
    public int Stac;
    [Range(1,10f)]
    public int Count;
    [Range(0.01f, 10f)]
    public float Weight;
}
