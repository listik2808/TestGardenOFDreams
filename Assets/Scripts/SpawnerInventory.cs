using Scripts.Data;
using Scripts.Infrastructure.Services.PersistenProgress;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts
{
    public class SpawnerInventory :MonoBehaviour ,ISavedProgress
    {
        [Range(1f, 30f)]
        [SerializeField] private int _countSlots;
        [SerializeField] private int _openSlot = 15;
        [SerializeField] private Transform _inventoryTransform;
        [SerializeField] private GameObject _slotPrefab;
        [SerializeField] private List<InventoryCell> _inventoryCells = new List<InventoryCell>();

        public Transform InventoryTransform => _inventoryTransform;
        public int CountSlots => _countSlots;
        public int OpenSlot => _openSlot;
        public List<InventoryCell> InventoryCells => _inventoryCells;

        public void SetCellInventory(InventoryCell cell)
        {
            _inventoryCells.Add(cell);
        }

        //private void Start()
        //{
        //    if(_inventoryCells == null|| _inventoryCells.Count == 0)
        //        FillInventory();
        //}

        //public void FillInventory()
        //{
        //    for (int i = 0; i < _countSlots; i++)
        //    {
        //        GameObject cell = Instantiate(_slotPrefab, _inventoryTransform);
        //        if(cell.TryGetComponent(out InventoryCell cellCell))
        //        {
        //            _inventoryCells.Add(cellCell);
        //        }
        //    }
        //}

        public void UpadeteProgress(PlayerProgress playerProgress)
        {
            Debug.Log("333");
            playerProgress.WorldData.LevelScene = new NameSceneI(CurrentLevel());
            //playerProgress.CellInventory.InventoryCells = _inventoryCells;
        }

        private string CurrentLevel() => 
            SceneManager.GetActiveScene().name;

        public void LoadProgress(PlayerProgress playerProgress)
        {
            Debug.Log("222");
            if(CurrentLevel() == playerProgress.WorldData.LevelScene.Level)
            {
                List<InventoryCell> saveCellsInventory = playerProgress.CellInventory.InventoryCells;
                if (saveCellsInventory != null)
                {
                    _inventoryCells = saveCellsInventory;
                }
                
            }
        }
    }
}
