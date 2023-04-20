using Scripts.Infrastructure.Services;
using UnityEngine;

namespace Scripts.Infrastructure.AssetManagement
{
    public interface IAsset : IService
    {
        GameObject Instantiate(string path);
    }
}