using Scripts.Data;
using Scripts.Infrastructure.Services.PersistenProgress;
using Scripts.Infrastructure.Services.SaveLoade;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;
using Random = UnityEngine.Random;

namespace Scripts
{
    public class Inventory :MonoBehaviour ,ISavedProgress
    {
        [Range(1f, 30f)]
        [SerializeField] private int _countSlots;
        [SerializeField] private int _openSlot = 15;
        [SerializeField] private Transform _inventoryTransform;
        private Item _item;
        private string _json;
 
        private List<InventoryCell> _inventoryCells = new List<InventoryCell>();
        private List<Item> _items = new List<Item>();
        private List<int> _idItems = new List<int>();

        public Transform InventoryTransform => _inventoryTransform;
        public int CountSlots => _countSlots;
        public int OpenSlot => _openSlot;
        public List<InventoryCell> InventoryCells => _inventoryCells;

        public event Action IsActivChanged;

        public void SetCellInventory(InventoryCell inventoryCell)
        {
            _inventoryCells.Add(inventoryCell);
        }

        public void SetOpenSlote(int openslote)
        {
            _openSlot = openslote;
        }

        public void DeletItem()
        {
            WriteOccupiedCells(_idItems);
            int random = Random.Range(0, _idItems.Count);
            int id = _idItems[random];
            ClirItem(id);
            _idItems.Clear();
        }

        public void Attac()
        {
            RecordingCellsPatron(_idItems);
            if(_idItems.Count > 0)
            {
                int random = Random.Range(0, _idItems.Count);
                int id = _idItems[random];
                DeletCartridge(id);
                _idItems.Clear();
            }
        }

        private void RecordingCellsPatron(List<int> idItems)
        {
            foreach (var item in _inventoryCells)
            {
                if (item.CellItem != null && item.CellItem.ItemType == TypeItem.Consumables)
                {
                    idItems.Add(item.Id);
                }
            }
        }

        private void DeletCartridge(int id)
        {
            foreach (var item in _inventoryCells)
            {
                if (item.Id == id)
                {
                    item.TakeCartridge();
                }
            }
        } 

        private void ClirItem(int id)
        {
            foreach (var item in _inventoryCells)
            {
                if (item.Id == id)
                {
                    item.Clir();
                }
            }
        }

        private void WriteOccupiedCells(List<int> idItems)
        {
            foreach (var item in _inventoryCells)
            {
                if(item.CellItem != null)
                {
                    idItems.Add(item.Id);
                }
            }
            if(idItems.Count <= 0)
            {
                Debug.LogError("Все слоты пусты");
            }
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.WorldData.LevelScene = new NameSceneI(CurrentLevel());
            progress.CellInventory.CountSlots = _countSlots;
            progress.CellInventory.OpenSlots = _openSlot;
            _json = JsonUtility.ToJson(progress.CellInventory);
            SaveLoad.Save(_json);
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _json = SaveLoad.Load();
            if(_json != null)
            {
                progress = JsonUtility.FromJson<PlayerProgress>(_json);
                _openSlot = progress.CellInventory.CurrentId;
                int countSlot = progress.CellInventory.CountSlots;
                if (countSlot != 0)
                {
                    _countSlots = countSlot;
                }
            }
        }

        public void SetAmmo(List<Ammo> ammo)
        {
            _items = new List<Item>();
            foreach (var predmet in ammo)
            {
                _items.Add(predmet);
            }
            CellSearch(_items);
            _items.Clear();
        }

        public void SetItems(List<Item> items)
        {
            CellSearch(items);
        }

        private void CellSearch(List<Item> items)
        {
            foreach (var item in items)
            {
                foreach (var cell in _inventoryCells)
                {
                    if (!cell.IsActiv)
                    {
                        break;
                    }
                    else if (cell.CellItem == null || cell.CellItem.NameItem == item.NameItem && !cell.IsFull)
                    {
                        cell.AssignItemCell(item);
                        break;
                    }
                }
            }
            SlotFullnessCheck();
        }

        private string CurrentLevel() => 
            SceneManager.GetActiveScene().name;

        private void SlotFullnessCheck()
        {
            int countFull = 0;
            foreach (var item in _inventoryCells)
            {
                if (item.IsFull)
                {
                    countFull++;
                }
            }
            if(countFull < _openSlot)
            {
                IsActivChanged?.Invoke();
            }
        }
    }
}
