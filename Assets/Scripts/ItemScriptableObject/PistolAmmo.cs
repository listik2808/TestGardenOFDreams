using UnityEngine;

[CreateAssetMenu(fileName = "PistolAmmo", menuName = "Ammo/PistolAmmo", order = 60)]
public class PistolAmmo : Ammo
{
    [Range(1, 30f)]
    [SerializeField] private int _maxCount;
    [Range(1, 10f)]
    [SerializeField] private int _countItem;
    [SerializeField, Min(0.01f)] private float _weight;

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
