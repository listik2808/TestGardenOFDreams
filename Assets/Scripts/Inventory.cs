using Scripts.Data;
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
        [SerializeField] private GameObject _slotPrefab;
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

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.WorldData.LevelScene = new NameSceneI(CurrentLevel());
            progress.CellInventory.CountSlots = _countSlots;
            progress.CellInventory.OpenSlots = _openSlot;
            json = JsonUtility.ToJson(progress.CellInventory);
            Debug.Log(progress.CellInventory.CountSlots + "Inventory UpdateProgress");
            SaveLoad.Save(json);
        }

        private string CurrentLevel() => 
            SceneManager.GetActiveScene().name;

        public void LoadProgress(PlayerProgress progress)
        {
            json = SaveLoad.Load();
            if(json != null)
            {
                progress = JsonUtility.FromJson<PlayerProgress>(json);
                _openSlot = progress.CellInventory.CurrentId;
                Debug.Log(_openSlot + "Inventory  _openSlot");
                int countSlot = progress.CellInventory.CountSlots;
                Debug.Log(countSlot + "Inventory countSlot");
                if (countSlot != 0)
                {
                    _countSlots = countSlot;
                }
            }
        }
    }
}
