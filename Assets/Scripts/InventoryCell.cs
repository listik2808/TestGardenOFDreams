using Scripts.Buttons;
using Scripts.Data;
using Scripts.Infrastructure.Services;
using Scripts.Infrastructure.Services.PersistenProgress;
using Scripts.Infrastructure.Services.SaveLoade;
using Scripts.ItemScriptableObject.Abstractitem;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Scripts
{
    public class InventoryCell : MonoBehaviour, ISavedProgress, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Image _iconSlot;
        [SerializeField] private TMP_Text _countStacText;
        [SerializeField] private ButtonBuyingSlot _buttonBuyingSlot;

        private ISaveLoadService _saveLoadService;
        private Mouse _mouse;
        private Item _item;
        private int _id;
        private int _currentCountItem;
        private int _maxCountItems;
        private float _weightCurrentItems;
        private bool _isActiv = false;
        private bool _isFull = false;
        private string _json;
        private int _count = 1;
        private bool _isDragging;
        private bool _isEntered;

        public Sprite Icon => _iconSlot.sprite;
        public bool IsActiv => _isActiv;
        public Item CellItem => _item;
        public bool IsFull => _isFull;
        public int Id => _id;

        private void OnEnable()
        {
            _buttonBuyingSlot.OnBought += Activate;
        }

        private void OnDisable()
        {
            _buttonBuyingSlot.OnBought -= Activate;
        }

        private void Awake()
        {
            _countStacText.enabled = false;
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
        }

        public void SetItem(InventoryCell inventoryCell)
        {
            _iconSlot.sprite = inventoryCell.Icon;
            _iconSlot.color = Color.white;
            _currentCountItem = inventoryCell._currentCountItem;
            _maxCountItems = inventoryCell._maxCountItems;
            _weightCurrentItems = inventoryCell._weightCurrentItems;
            _isFull = inventoryCell._isFull;
            _item = inventoryCell.CellItem;
            ShowCountItem();
        }

        public void SetMouse(Mouse mouse) =>
            _mouse = mouse;

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
            if (_currentCountItem == 0)
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
            _countStacText.enabled = false;
            _iconSlot.color = Color.clear;
            ShowCountItem();
        }

        public void AssignId(int id)
        {
            _id = id;
        }

        public void Activate()
        {
            _isActiv = true;
            _buttonBuyingSlot.Diactivate();
        }

        public void LoadProgress(PlayerProgress progress)
        {
            string json = SaveLoad.Load();
            if (json != null)
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

        public void OnPointerEnter(PointerEventData eventData)
        {
            _isEntered = true;
            _mouse.ActivateEnteredCell(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _isEntered = false;
            _mouse.DiactivateEnteredCell();
        }

        public void OnDrag(PointerEventData eventData)
        {

        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _isDragging = true;
            _mouse.DraggingActivate(this);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_isDragging && _mouse.DraggingCell.CellItem != null && _mouse.EnteredCell != null && _mouse.EnteredCell.CellItem == null && _mouse.EnteredCell._isActiv)
            {
                SettingItemUsingDrag();
                _saveLoadService.SaveProgress();
            }
            else if (_isDragging && _mouse.DraggingCell.CellItem != null && _mouse.EnteredCell != null && _mouse.EnteredCell.CellItem != null && NameCheckDrag() && CapacityCheckDrag() && CheckId())
            {
                _mouse.EnteredCell.ConnectElements(this);
                Clir();
                _saveLoadService.SaveProgress();
            }
            else if (_isDragging)
            {
                _mouse.Reset();
                _isDragging = false;
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
            progress.CellInventory.AddSpriteItem(_iconSlot.sprite, _id);
            progress.CellInventory.AddCurrentCoutItem(_currentCountItem, _id);
            progress.CellInventory.AddItem(_item, _id);
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
            if (_item != null)
            {
                _iconSlot.sprite = _item.Icon;
                _iconSlot.color = Color.white;
                _currentCountItem = _item.GetStacItem();
                SetMaxCountItems();
            }
        }

        private void SetMaxCountItems() =>
            _maxCountItems = _item.GetMaxCount();

        private void AddWeight() =>
            _weightCurrentItems = _item.AddWeightItem(_currentCountItem);

        private void ShowCountItem()
        {
            _countStacText.text = _currentCountItem.ToString();
            if (_currentCountItem > 1)
                _countStacText.enabled = true;
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
                if (_id - _count == i)
                {
                    _currentCountItem = progress.CellInventory.CurrentCountItem[i];
                }
            }
        }

        private void LoadSpriteItems(PlayerProgress progress)
        {
            for (int i = 0; i < progress.CellInventory.Sprite.Count; i++)
            {
                if (_id - _count == i)
                {
                    _iconSlot.sprite = progress.CellInventory.Sprite[i];
                    _iconSlot.color = Color.white;
                }
            }
        }

        private void LoadItems(PlayerProgress progress)
        {
            for (int i = 0; i < progress.CellInventory.Items.Count; i++)
            {
                if (_id - _count == i)
                {
                    _item = progress.CellInventory.Items[i];
                }
            }
        }

        private bool CheckId() =>
            _mouse.DraggingCell.Id != _mouse.EnteredCell.Id;

        private void SettingItemUsingDrag()
        {
            Item item = _mouse.EnteredCell.CellItem;
            _mouse.EnteredCell.SetItem(this);
            _item = item;
            Clir();
            ShowCountItem();
            _mouse.DraggingDeactivate();
            _isDragging = false;
        }

        private bool NameCheckDrag() =>
            _mouse.EnteredCell.CellItem.NameItem == _mouse.DraggingCell.CellItem.NameItem;

        private bool CapacityCheckDrag() =>
            _mouse.EnteredCell._currentCountItem + _mouse.DraggingCell._currentCountItem <= _mouse.EnteredCell._maxCountItems;

        private void ConnectElements(InventoryCell inventoryCell)
        {
            _currentCountItem += inventoryCell._currentCountItem;
            _weightCurrentItems += inventoryCell._weightCurrentItems;
            SetMaxCountItems();
            CheckCompleteness();
            ShowCountItem();
            _mouse.DraggingDeactivate();
        }
    }
}