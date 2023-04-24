using Scripts.Data;
using Scripts.Infrastructure.Services.PersistenProgress;
using Scripts.Infrastructure.Services.SaveLoade;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour,ISavedProgress
{
    [SerializeField] private Image _iconSlot;
    [SerializeField] private bool _isActiv = false;
    [SerializeField] private TMP_Text _countStacText;
    private Item _currentItem;
    private int _id;
    private int _currentCountItem;
    private int _maxCountPatron;
    private float _weightCurrentPatrons;
    private bool _isFull = false;

    public bool IsActiv => _isActiv;
    public Item CellItem => _currentItem;
    public bool IsFull => _isFull;
    public int MaxCountItem => _maxCountPatron;

    private void Awake()
    {
        _countStacText.alpha = 0;
    }

    public void AssignItemCell(Item item)
    {
        if (_currentItem == null)
        {
            _currentItem = item;
            AcceptData();
        }
        else
        {
            _currentCountItem += _currentItem.GetStacPatron();
            CheckCompleteness();
        }
        AddWeight();
        ShowCountItem();
    }

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
    }

    public void UpdateProgress(PlayerProgress progress)
    {
        if (_isActiv == true)
        {
            progress.CellInventory.AddId(_id);
            string json = JsonUtility.ToJson(progress.CellInventory.Id);
            SaveLoad.Save(json);
        }
    }

    private void AcceptData()
    {
        _iconSlot.sprite = _currentItem.Icon;
        _currentCountItem = _currentItem.GetStacPatron();
        _maxCountPatron = _currentItem.GetMaxCount(_currentCountItem);
    }

    private void AddWeight()
    {
        _weightCurrentPatrons = _currentItem.AddWeightItem(_currentCountItem);
    }

    private void ShowCountItem()
    {
        _countStacText.text = _currentCountItem.ToString();
        if (_currentCountItem > 1)
            _countStacText.alpha = 100;
    }

    private void CheckCompleteness()
    {
        int stac = _currentItem.GetStacPatron();
        int newcount = _currentCountItem + stac;
        if (newcount <= _maxCountPatron)
        {
            _isFull = false;
        }
        else
        {
            _isFull = true;
        }
    }
}
