using UnityEngine;

namespace Scripts.Infrastructure.AssetManagement
{
    public class AssetProvider : IAsset
    {
        public GameObject Instantiate(string path)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }

    }
}
