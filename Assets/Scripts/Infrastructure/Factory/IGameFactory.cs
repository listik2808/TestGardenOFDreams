using Scripts.Infrastructure.Services;
using Scripts.Infrastructure.Services.PersistenProgress;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        void Cleanup();
        GameObject CreateHud();
        void CreateCellInventary();
    }
}