using Scripts.Data;
using Scripts.Infrastructure.Services.PersistenProgress;
using UnityEngine;

public class InventoryCell : MonoBehaviour,ISavedProgress
{
    [SerializeField] private Item _currentItem;
    [SerializeField] private bool _isActiv = false;

    public bool IsActiv => _isActiv;

    public void Activate()
    {
        _isActiv = true;
    }

    public void Deactivate() 
    {
        _isActiv = false;
    }

    public void LoadProgress(PlayerProgress playerProgress)
    {
    }

    public void UpadeteProgress(PlayerProgress playerProgress)
    {
        playerProgress.CellInventory.InventoryCells.Add(this);
        Debug.Log(1);
    }
}
