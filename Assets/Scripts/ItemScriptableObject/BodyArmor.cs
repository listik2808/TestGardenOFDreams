using Scripts.ItemScriptableObject.Abstractitem;
using UnityEngine;

[CreateAssetMenu(fileName = "BodyArmor", menuName = "Torso/BodyArmor", order = 65)]
public class BodyArmor : Torso
{
    [Range(1, 50)]
    [SerializeField] private int _torsoProtection;
    [Range(1, 5f)]
    [SerializeField] private int _maxCount;
    [SerializeField, Min(1)] private int _countItem;
    [SerializeField, Min(1f)] private float _weight;

    public override float AddWeightItem(int currentCount)
    {
        return _weight * currentCount;
    }

    public override int GetMaxCount()
    {
        return _maxCount;
    }

    public override int GetStacItem()
    {
        return _countItem;
    }
}
