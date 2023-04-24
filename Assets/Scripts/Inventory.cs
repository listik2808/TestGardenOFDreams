﻿using Scripts.Data;
using Scripts.Infrastructure.Services.PersistenProgress;
using Scripts.Infrastructure.Services.SaveLoade;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts
{
    public class Inventory :MonoBehaviour ,ISavedProgress
    {
        [Range(1f, 30f)]
        [SerializeField] private int _countSlots;
        [SerializeField] private int _openSlot = 15;
        [SerializeField] private Transform _inventoryTransform;
        private Item _item;
        private string json;
        public List<InventoryCell> _inventoryCells = new List<InventoryCell>();

        public Transform InventoryTransform => _inventoryTransform;
        public int CountSlots => _countSlots;
        public int OpenSlot => _openSlot;
        public List<InventoryCell> InventoryCells => _inventoryCells;

        public void SetCellInventory(InventoryCell inventoryCell)
        {
            _inventoryCells.Add(inventoryCell);
        }

        public void SetOpenSlote(int openslote)
        {
            _openSlot = openslote;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.WorldData.LevelScene = new NameSceneI(CurrentLevel());
            progress.CellInventory.CountSlots = _countSlots;
            progress.CellInventory.OpenSlots = _openSlot;
            json = JsonUtility.ToJson(progress.CellInventory);
            Debug.Log(progress.CellInventory.CountSlots + "Inventory UpdateProgress");
            SaveLoad.Save(json);
        }

        public void LoadProgress(PlayerProgress progress)
        {
            json = SaveLoad.Load();
            if(json != null)
            {
                progress = JsonUtility.FromJson<PlayerProgress>(json);
                _openSlot = progress.CellInventory.CurrentId;
                int countSlot = progress.CellInventory.CountSlots;
                if (countSlot != 0)
                {
                    _countSlots = countSlot;
                }
            }
        }

        public void SetPatron(List<Item> items)
        {
            foreach (var item in items)
            {
                foreach (var cell in _inventoryCells)
                {
                    if (!cell.IsActiv)
                    {
                        return;
                    }
                    else if (cell.CellItem == null || cell.CellItem.TypeItem == item.TypeItem && !cell.IsFull)
                    {
                        cell.AssignItemCell(item);
                        break;
                    }
                }
            }
        }

        private string CurrentLevel() => 
            SceneManager.GetActiveScene().name;
    }
}
