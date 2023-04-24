using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Data
{
    [Serializable]
    public class CellInventory
    {
        public int OpenSlots;
        public List<int> Id = new List<int>();
        public int CountSlots;
        public int CurrentId;
        public List<Sprite> Sprite = new List<Sprite>();
        public List<int> MaxCountItem = new List<int>();
        public List<int> CurrentCountItem = new List<int>();
        public List<InventoryCell> InventoryCells = new List<InventoryCell>();
        public List<Item> Items = new List<Item>();
        private List<int> newId = new List<int>();

        public void AddId(int id)
        {
            if (Id.Count >= id)
                RewritingId(id, Id);
            else
                IncreaseIdList(id);
        }

        public void AddCurrentCoutItem( int currentCount,int id)
        {
            if (CurrentCountItem.Count >= id)
                RewritingPatron(id, currentCount);
            else
                CurrentCountItem.Add(currentCount);
        }

        public void AddSpriteItem(Sprite sprite,int id)
        {
            if (Sprite.Count >= id)
                RewritingSprite(id, sprite);
            else
                Sprite.Add(sprite);
        }

        public void AddItem(Item item,int id)
        {
            if (Items.Count >= id)
                RewritingItem(id, item);
            else
                Items.Add(item);
        }

        private void RewritingId(int id,List<int> countItem)
        {
            for (int i = 0; i < countItem.Count; i++)
            {
                if (Id[i] == id)
                {
                    Id[i] = id;
                    break;
                }
            }
        }

        private void RewritingPatron(int id, int countItem)
        {
            for (int i = 0; i < CurrentCountItem.Count; i++)
            {
                if (Id[i] == id)
                {
                    CurrentCountItem[i] = countItem;
                    break;
                }
            }
        }
        private void RewritingSprite(int id, Sprite sprite)
        {
            for (int i = 0; i < Sprite.Count; i++)
            {
                if (Id[i] == id)
                {
                    Sprite[i] = sprite;
                    break;
                }
            }
        }

        private void RewritingItem(int id, Item item)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Id[i] == id)
                {
                    Items[i] = item;
                    break;
                }
            }
        }

        private void IncreaseIdList(int id)
        {
            Id.Add(id);
            CurrentId = Id.Count;
        }
    }
}
