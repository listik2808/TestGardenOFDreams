using Scripts.Infrastructure.AssetManagement;
using UnityEngine;

namespace Scripts.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAsset _asset;

        public GameFactory(IAsset asset)
        {
            _asset = asset;
        }

        public GameObject CreateHud() =>
            _asset.Instantiate(AssetPath.HadPath);
    }
}
