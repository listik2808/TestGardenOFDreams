using System;

namespace Scripts.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public KillData KillData;
        public WorldData WorldData;
        public CellInventory CellInventory;

        public PlayerProgress(string initialLevel) 
        {
            WorldData = new WorldData(initialLevel);
            KillData = new KillData();
            CellInventory = new CellInventory();
        }
    }
}
