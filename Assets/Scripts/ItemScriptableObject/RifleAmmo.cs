using UnityEngine;

[CreateAssetMenu(fileName = "RifleAmmo", menuName = "Ammo/RifleAmmo", order = 61)]
public class RifleAmmo : Ammo
{
    [Range(1, 60f)]
    [SerializeField] private int _maxCount;
    [Range(1, 15f)]
    [SerializeField] private int _countItem;
    [SerializeField, Min(0.01f)] private float _weight;

    public override float AddWeightItem(int currentCount)
    {
        float weight = _weight * currentCount;
        return weight;
    }

    public override int GetStacItem()
    {
        return _countItem;
    }

    public override int  GetMaxCount()
    {
        return _maxCount;
    }
}