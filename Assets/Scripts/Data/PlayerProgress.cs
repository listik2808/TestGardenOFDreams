using System;

namespace Scripts.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData;
        public CellInventory CellInventory;
        public ButtonBuySlote buttonBuySlote;

        public PlayerProgress(string initialLevel) 
        {
            WorldData = new WorldData(initialLevel);
            CellInventory = new CellInventory();
            buttonBuySlote = new ButtonBuySlote();
        }
    }
}
