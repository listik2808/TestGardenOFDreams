using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PistolAmmo", menuName = "ScriptableObject/PistolAmmo", order = 60)]
public class PistolAmmo : Item
{
    [Range(1, 30f)]
    [SerializeField] private int _maxCount;
    [Range(1, 10f)]
    [SerializeField] private int _countItem;
    [SerializeField, Min(0.01f)] private float _weight;

    public override float AddWeightItem(int currentCount)
    {
        float weight = _weight * currentCount;
        return weight;
    }

    public override int GetStacPatron()
    {
        return _countItem;
    }

    public override int GetMaxCount()
    {
        return _maxCount;
    }
}