using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItem",menuName = "ScriptableObject/InventoryItem", order = 61)]
public class Item : ScriptableObject
{
    public string Name = "Item";
    public Sprite Icon;
    public GameObject Prefab;
    public int Stac;
}
