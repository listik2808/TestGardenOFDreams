using Scripts.Data;

namespace Scripts.Infrastructure.Services.SaveLoade
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
    }
}