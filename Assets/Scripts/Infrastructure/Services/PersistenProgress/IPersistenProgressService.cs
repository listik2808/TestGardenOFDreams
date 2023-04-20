using Scripts.Data;

namespace Scripts.Infrastructure.Services.PersistenProgress
{
    public interface IPersistenProgressService : IService
    {
        PlayerProgress Progress { get; set; }
    }
}