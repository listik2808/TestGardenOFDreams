using Scripts.Infrastructure.AssetManagement;
using Scripts.Infrastructure.Services.PersistenProgress;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Scripts.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAsset _asset;
        private  SpawnerInventory _spawnerInventory;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        public GameFactory(IAsset asset)
        {
            _asset = asset;
        }

        public GameObject CreateHud() =>
            InstaitiateRegistered(AssetPath.HadPath);

        public void CreateCellInventary() => 
            InstaitiateRegistered(AssetPath.CellPath, _spawnerInventory.InventoryTransform);

        private GameObject InstaitiateRegistered(string prefabPath)
        {
            GameObject gameObject = _asset.Instantiate(prefabPath);

            if (gameObject.TryGetComponent(out Hud hud))
                _spawnerInventory = hud.SpawnerInventory;

            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private void InstaitiateRegistered(string prefabPath, Transform at)
        {
            for (int i = 0; i < _spawnerInventory.CountSlots; i++)
            {
                Debug.Log(i);
                GameObject inventary = _asset.Instantiate(prefabPath, at);
                if(inventary.TryGetComponent(out InventoryCell inventoryCell))
                {
                    _spawnerInventory.SetCellInventory(inventoryCell);
                    RegisterProgressWatchers(inventary);
                }
                
            }
            ActivateSlots();
        }

        private void ActivateSlots()
        {
            List<InventoryCell> cell = _spawnerInventory.InventoryCells;
            for (int i = 0; i < _spawnerInventory.OpenSlot; i++)
            {
                cell[i].Activate();
            }
            
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        private void Register(ISavedProgressReader progressReader)
        {
            if(progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }

        
    }
}
