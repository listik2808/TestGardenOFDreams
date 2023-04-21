using Scripts.Data;

namespace Scripts.Infrastructure.Services.PersistenProgress
{
    public interface ISavedProgressReader
    {
        void LoadProgress(PlayerProgress playerProgress);
    }

    public interface ISavedProgress : ISavedProgressReader
    {
        void UpadeteProgress(PlayerProgress playerProgress);
    }
}
