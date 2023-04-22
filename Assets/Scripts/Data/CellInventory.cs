using System;
using System.Collections.Generic;

namespace Scripts.Data
{
    [Serializable]
    public class CellInventory
    {
        public int OpenSlots;
        public List<int> Id;
        public int CountSlots;
        public int CurrentId;
        public List<InventoryCell> InventoryCells = new List<InventoryCell>();
        private List<int> newId = new List<int>();

        public CellInventory() 
        {
            Id= new List<int>();
        }

        public void AddId(int id)
        {
            Id.Add(id);
            CurrentId = Id.Count;
        }

        public int GetId()
        {
            newId = Id;
            int number = newId[0];
            newId.Remove(0);
            Id = newId;
            return number;
        }
    }
}
