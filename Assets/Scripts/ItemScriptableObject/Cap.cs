using UnityEngine;

[CreateAssetMenu(fileName = "Cap", menuName = "Head/Cap", order = 66)]
public class Cap : Head
{
    [Range(1, 50)]
    [SerializeField] private int _headProtection;
    [Range(1, 5f)]
    [SerializeField] private int _maxCount;
    [SerializeField, Min(1)] private int _countItem;
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
