using System;
using System.Collections.Generic;
using System.Linq;

namespace TileGame.Character
{
    public class Inventory<T>
    {
        private readonly int slotCount;
        private readonly T[] contents;

        public Inventory(int slotCount)
        {
            this.slotCount = slotCount;
            this.contents = new T [slotCount];
        }

        public bool SetContent(T entity, int slotNumber)
        {
            if (this.contents.Length < slotNumber)
            {
                return false;
            }

            this.contents[slotNumber] = entity;
            return true;
        }

        public T GetContent(int slotNumber)
        {
            if (this.slotCount == 0 || this.contents.Length < slotNumber)
            {
                throw new ArgumentException("Parameter cannot be null");
            }

            return this.contents[slotNumber];
        }
    }
}