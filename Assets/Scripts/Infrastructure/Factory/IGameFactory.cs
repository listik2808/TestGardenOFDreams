using Scripts.Infrastructure.Services;
using UnityEngine;

namespace Scripts.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateHud();
    }
}