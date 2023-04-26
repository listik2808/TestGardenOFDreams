using Scripts.Data;
using Scripts.Infrastructure.AssetManagement;
using Scripts.Infrastructure.Services.PersistenProgress;
using Scripts.Infrastructure.Services.SaveLoade;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAsset _asset;
        private readonly IPersistenProgressService _progressService;
        private readonly int StartId = 1;
        private  Inventory _inventory;
        private Hud _hud;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        public GameFactory(IAsset asset, IPersistenProgressService progressService)
        {
            _asset = asset;
            _progressService = progressService;
        }

        public GameObject CreateHud() =>
            InstaitiateRegistered(AssetPath.HadPath);

        public void CreateCellInventary() => 
            InstaitiateRegistered(AssetPath.CellPath, _inventory.InventoryTransform);

        private GameObject InstaitiateRegistered(string prefabPath)
        {
            GameObject gameObject = _asset.Instantiate(prefabPath);
            ComponentSearch(gameObject);

            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private void ComponentSearch(GameObject gameObject)
        {
            _hud = gameObject.GetComponent<Hud>();
            _inventory = _hud.Inventory;
            AmmoDepot ammoDepot = _hud.gameObject.GetComponentInChildren<AmmoDepot>();
            ButtonAddRandomItems buttonAddRandom = _hud.gameObject.GetComponentInChildren<ButtonAddRandomItems>();
            ButtonDeletItemCell buttonDeletItemCell = _hud.gameObject.GetComponentInChildren<ButtonDeletItemCell>();
            _hud.SetComponent(ammoDepot, buttonAddRandom, buttonDeletItemCell);
            GetOpensSlots();
        }
        //Надо найти этому место
        private void GetOpensSlots()
        {
            string json = SaveLoad.Load();
            if(json != null)
            {
                _progressService.Progress = JsonUtility.FromJson<PlayerProgress>(json);
                _inventory.SetOpenSlote(_progressService.Progress.CellInventory.CurrentId);
            }
        }

        private void InstaitiateRegistered(string prefabPath, Transform at)
        {
            for (int i = 0; i < _inventory.CountSlots; i++)
            {
                GameObject inventary = _asset.Instantiate(prefabPath, at);
                if(inventary.TryGetComponent(out InventoryCell inventoryCell))
                {
                    inventoryCell.AssignId(i + StartId);
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
