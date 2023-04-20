

using Scripts.Data;

namespace Scripts.Infrastructure.Services.PersistenProgress
{
    public class PersistenProgressService : IPersistenProgressService
    {
        public PlayerProgress Progress { get; set; }
    }
}
