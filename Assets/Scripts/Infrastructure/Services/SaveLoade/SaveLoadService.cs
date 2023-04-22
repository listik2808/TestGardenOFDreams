using Scripts.Data;
using Scripts.Infrastructure.Factory;
using Scripts.Infrastructure.Services.PersistenProgress;
using UnityEngine;

namespace Scripts.Infrastructure.Services.SaveLoade
{
    public class SaveLoadService : ISaveLoadService
    {
        private readonly IPersistenProgressService _progressService;
        private readonly IGameFactory _gameFactory;

        private string _json;

        public SaveLoadService(IPersistenProgressService progressService, IGameFactory gameFactory)
        {
            _progressService = progressService;
            _gameFactory = gameFactory;
            SaveLoad.Init();
        }

        public void SaveProgress()
        {
            foreach (ISavedProgress progressWriter in _gameFactory.ProgressWriters)
            {
                progressWriter.UpdateProgress(_progressService.Progress);
            }
            _json = JsonUtility.ToJson(_progressService.Progress);
            SaveLoad.Save(_json);
        }

        public PlayerProgress LoadProgress()
        {
            if (_json != null)
            {
                _json = SaveLoad.Load();
                _progressService.Progress = JsonUtility.FromJson<PlayerProgress>(_json);
                return _progressService.Progress;
            }
            return _progressService.Progress;
        }
    }
}