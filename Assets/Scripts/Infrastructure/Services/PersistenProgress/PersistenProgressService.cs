using Scripts.Data;
using System;

namespace Scripts.Infrastructure.Services.PersistenProgress
{
    public class PersistenProgressService : IPersistenProgressService
    {
        public PlayerProgress Progress { get; set; }
    }
}
