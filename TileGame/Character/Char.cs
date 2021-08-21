using System;
using SFML.Graphics;
using TileGame.Interfaces;
using TileGame.Level;
using TileGame.Pathfinding;

namespace TileGame.Character
{
    public class Char : IUpdate, ITick
    {
        public Char(ItemInventory itemInventory)
        {
            ItemInventory = itemInventory;
        }

        public uint Identifier { get; set; } = 0;
        public ItemInventory ItemInventory { get; set; }
        public int Strength { get; set; } = 0;
        public float StrengthMulti { get; private set; } = 1.5f;
        private float MaxWeight { get; set; }
        public float CurrentWeight { get; private set; } = 0;
        public Node OccupiedNode;

        public Sprite Sprite { get; set; }

        public virtual void Reconstruct()
        {
            MaxWeight = Strength * StrengthMulti;
        }

        public bool CanMove { get; set; } = true;

        public virtual void Update()
        {
            ItemInventory.Update();
        }

        public virtual void Tick()
        {
        }

        
    }
}