using Scripts.Data;
using Scripts.Infrastructure.Services.PersistenProgress;
using Scripts.Infrastructure.Services.SaveLoade;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryCell : MonoBehaviour,ISavedProgress
{
    [SerializeField] private List<Item> _currentItem = new List<Item>();
    [SerializeField] private bool _isActiv = false;
    [SerializeField] private TMP_Text _countStac;
    private int _id;

    public bool IsActiv => _isActiv;
    public int Id => _id;

    public void AssignId(int id)
    {
        if(id > 0)
            _id = id;
    }

    public void Activate()
    {
        _isActiv = true;
    }

    public void Deactivate() 
    {
        _isActiv = false;
    }

    public void LoadProgress(PlayerProgress progress)
    {
        string json = SaveLoad.Load();
        progress = JsonUtility.FromJson<PlayerProgress>(json);
        _id = progress.CellInventory.GetId();
        Debug.Log(_id + "Устанавливаю _id");
    }

    public void UpdateProgress(PlayerProgress progress)
    {
        Debug.Log(_id);
        if (_isActiv == true)
        {
            Debug.Log(_id + "Вошел");
            progress.CellInventory.AddId(_id);
            string json = JsonUtility.ToJson(progress.CellInventory.Id);
            SaveLoad.Save(json);
            Debug.Log(progress.CellInventory.Id + " Сохранить _id");
        }
    }
}
