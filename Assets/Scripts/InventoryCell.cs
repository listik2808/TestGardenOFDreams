using Scripts.Data;
using Scripts.Infrastructure.Services.PersistenProgress;
using Scripts.Infrastructure.Services.SaveLoade;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour,ISavedProgress
{
    [SerializeField] private Image _iconSlot;
    [SerializeField] private bool _isActiv = false;
    [SerializeField] private TMP_Text _countStacText;

    private Item _item;
    private int _id;
    private int _currentCountItem;
    private int _maxCountItems;
    private float _weightCurrentItems;
    private bool _isFull = false;
    private string _json;

    public bool IsActiv => _isActiv;
    public Item CellItem => _item;
    public bool IsFull => _isFull;
    public int Id => _id;

    private void Awake()
    {
        _countStacText.alpha = 0;
    }

    public void AssignItemCell(Item item)
    {
        if (_item == null)
        {
            _item = item;
            AcceptData();
        }
        else
        {
            AddStack();
        }
        AddWeight();
        ShowCountItem();
    }

    public void TakeCartridge()
    {
        _currentCountItem--;
        ShowCountItem();
        AddWeight();
        if(_currentCountItem == 0)
        {
            Clir();
        }
    }

    public void Clir()
    {
        _item = null;
        _currentCountItem = 0;
        _weightCurrentItems = 0;
        _isFull = false;
        _maxCountItems = 0;
        _iconSlot.sprite = null;
        _countStacText.alpha = 0;
        _iconSlot.enabled =false;
    }

    public void AssignId(int id)
    {
        _id = id;
    }

    public void Activate() => 
        _isActiv = true;

    public void LoadProgress(PlayerProgress progress)
    {
        string json = SaveLoad.Load();
        if(json != null)
        {
            progress = JsonUtility.FromJson<PlayerProgress>(json);
            LoadItems(progress);
            InstallCharacteristics(progress);
        }
    }

    public void UpdateProgress(PlayerProgress progress)
    {
        if (_isActiv == true)
        {
            SaveCharacteristics(progress);
        }
    }

    private void InstallCharacteristics(PlayerProgress progress)
    {
        if (_item != null)
        {
            LoadSpriteItems(progress);
            LoadCurentCountItems(progress);
            SetMaxCountItems();
            AddWeight();
            CheckCompleteness();
            ShowCountItem();
        }
    }

    private void SaveCharacteristics(PlayerProgress progress)
    {
        progress.CellInventory.AddId(_id);
        progress.CellInventory.AddSpriteItem(_iconSlot.sprite,_id);
        progress.CellInventory.AddCurrentCoutItem(_currentCountItem,_id);
        progress.CellInventory.AddItem(_item,_id);
        _json = JsonUtility.ToJson(progress.CellInventory);
        SaveLoad.Save(_json);
    }

    private void AddStack()
    {
        _currentCountItem += _item.GetStacItem();
        CheckCompleteness();
    }

    private void AcceptData()
    {
        _iconSlot.sprite = _item.Icon;
        _iconSlot.enabled = true;
        _currentCountItem = _item.GetStacItem();
        SetMaxCountItems();
    }

    private void SetMaxCountItems() => 
        _maxCountItems = _item.GetMaxCount();

    private void AddWeight() => 
        _weightCurrentItems = _item.AddWeightItem(_currentCountItem);

    private void ShowCountItem()
    {
        _countStacText.text = _currentCountItem.ToString();
        if (_currentCountItem > 1)
            _countStacText.alpha = 100;
    }

    private void CheckCompleteness()
    {
        int stac = _item.GetStacItem();
        int newcount = _currentCountItem + stac;
        if (newcount <= _maxCountItems)
            _isFull = false;
        else
            _isFull = true;
    }

    private void LoadCurentCountItems(PlayerProgress progress)
    {
        for (int i = 0; i < progress.CellInventory.CurrentCountItem.Count; i++)
        {
            if(_id - 1 == i)
            {
                _currentCountItem = progress.CellInventory.CurrentCountItem[i];
            }
        }
    }

    private void LoadSpriteItems(PlayerProgress progress)
    {
        for (int i = 0; i < progress.CellInventory.Sprite.Count; i++)
        {
            if(_id - 1 == i)
            {
                _iconSlot.sprite = progress.CellInventory.Sprite[i];
            }
        }
    }

    private void LoadItems(PlayerProgress progress)
    {
        for (int i = 0; i < progress.CellInventory.Items.Count; i++)
        {
            if (_id - 1 == i)
            {
                _item = progress.CellInventory.Items[i];
            }
        }
    }
}
