using UnityEngine;

public abstract class Item : ScriptableObject
{
    [SerializeField] protected NameItem ItemName;
    [SerializeField] protected TypeItem TypeItem;
    [SerializeField] protected Sprite _icon;

    public NameItem NameItem => ItemName;
    public TypeItem ItemType => TypeItem;
    public Sprite Icon => _icon;

    public abstract int GetStacItem();
    public abstract float AddWeightItem(int currentCount);
    public abstract int GetMaxCount();
}
