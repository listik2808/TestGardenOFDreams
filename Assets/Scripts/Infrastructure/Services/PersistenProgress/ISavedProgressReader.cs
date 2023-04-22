using Scripts.Data;

namespace Scripts.Infrastructure.Services.PersistenProgress
{
    public interface ISavedProgressReader
    {
        void LoadProgress(PlayerProgress progress);
    }
}
