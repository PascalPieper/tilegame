using System;
using System.Collections.Generic;

namespace TileGame.Level
{
    public class Inventory<T>
    {
        public Inventory(int maxSlots)
        {
            MaxSlots = maxSlots;
            _items = new List<T>(maxSlots);
        }

        private int MaxSlots { get; set; }

        private List<T> _items;

        public List<T> Items
        {
            get { return _items; }
            set
            {
                if (value.Count > MaxSlots)
                {
                    throw new Exception(this.GetType().Name +
                                        " The passed List surpasses the maximum Item limit set be the MaxSlots variable.");
                }

                Items = value;
            }
        }

        public T GetItemAtIndex(int index)
        {
            return Items[index];
        }

        public bool AddItemToFront(T item)
        {
            if (Items.Count < MaxSlots)
            {
                Items.Add(item);
                return true;
            }

            return false;
        }
    }
}