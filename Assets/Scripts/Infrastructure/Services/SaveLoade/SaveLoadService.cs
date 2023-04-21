using Scripts.Data;
using Scripts.Infrastructure.Factory;
using Scripts.Infrastructure.Services.PersistenProgress;
using UnityEngine;

namespace Scripts.Infrastructure.Services.SaveLoade
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";
        private readonly IPersistenProgressService _progressService;
        private readonly IGameFactory _gameFactory;

        private string _json;

        public SaveLoadService(IPersistenProgressService progressService, IGameFactory gameFactory)
        {
            _progressService = progressService;
            _gameFactory = gameFactory;
        }

        public void SaveProgress()
        {
            foreach (ISavedProgress progressWriters in _gameFactory.ProgressWriters)
            {
                progressWriters.UpadeteProgress(_progressService.Progress);
            }
            //_json = _progressService.Progress.ToJson();
            _json = JsonUtility.ToJson(_progressService.Progress.CellInventory.InventoryCells);
            Debug.Log(_json);
        }

        public PlayerProgress LoadProgress()
        {
            if(_json == null)
            {
                Debug.Log("11");
                PlayerProgress curent = JsonUtility.FromJson<PlayerProgress>(_json);
                //PlayerProgress curent = _json.ToDeserialized<PlayerProgress>();
                Debug.Log(curent);
                return curent;
            }
            return _progressService.Progress;
        }
    }
}