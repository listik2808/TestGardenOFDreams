using System;

namespace Scripts.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public OpenData OpenData;
        public WorldData WorldData;
        public CellInventory CellInventory;

        public PlayerProgress(string initialLevel) 
        {
            WorldData = new WorldData(initialLevel);
            OpenData = new OpenData();
            CellInventory = new CellInventory();
        }
    }
}
