using Scripts.Infrastructure.AssetManagement;
using Scripts.Infrastructure.Services.PersistenProgress;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAsset _asset;
        private  Inventory _inventory;
        private Hud _hud;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        public GameFactory(IAsset asset)
        {
            _asset = asset;
        }

        public GameObject CreateHud() =>
            InstaitiateRegistered(AssetPath.HadPath);

        public void CreateCellInventary() => 
            InstaitiateRegistered(AssetPath.CellPath, _inventory.InventoryTransform);

        private GameObject InstaitiateRegistered(string prefabPath)
        {
            GameObject gameObject = _asset.Instantiate(prefabPath);
            SetComponents(gameObject);

            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private void SetComponents(GameObject gameObject)
        {
            if (gameObject.TryGetComponent(out Hud hud))
            {
                //_hud = hud;
                _inventory = hud.inventory;
            }
        }

        private void InstaitiateRegistered(string prefabPath, Transform at)
        {
            for (int i = 0; i < _inventory.CountSlots; i++)
            {
                GameObject inventary = _asset.Instantiate(prefabPath, at);
                if(inventary.TryGetComponent(out InventoryCell inventoryCell))
                {
                    inventoryCell.AssignId(i + 1);
                    _inventory.SetCellInventory(inventoryCell);
                    RegisterProgressWatchers(inventary);
                }
            }
            ActivateSlots();
        }

        private void ActivateSlots()
        {
            for (int i = 0; i < _inventory.OpenSlot; i++)
            {
                _inventory.InventoryCells[i].Activate();
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
