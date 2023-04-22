using Scripts.Data;

namespace Scripts.Infrastructure.Services.PersistenProgress
{
    public interface ISavedProgress : ISavedProgressReader
    {
        void UpdateProgress(PlayerProgress progress);
    }
}
