using UnityEditor.PackageManager;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    [SerializeField] private TypeItem _typeItem;
    [SerializeField] private Sprite _icon;

    public TypeItem TypeItem => _typeItem;
    public Sprite Icon => _icon;

    public abstract int GetStacPatron();
    public abstract float AddWeightItem(int currentCount);
    public abstract int GetMaxCount();
}
