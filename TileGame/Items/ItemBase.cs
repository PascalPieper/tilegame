using SFML.Graphics;
using TileGame.Interfaces;

namespace TileGame.Items
{
    public class ItemBase : ITick
    {
        public uint Identifier { get; } = 0;
        public string Name { get; private set; }
        public string Description { get; private set; }
        public double Price { get; private set; }
        public Sprite Sprite { get; set; } = null;

        public ItemBase(string name, string description, double price, float weight, string textureName)
        {
            Name = name;
            Description = description;
            Price = price;
            _weight = weight;
        }

        public ItemBase()
        {
        }

        private float _weight;

        public float Weight
        {
            get => this._weight;
            private set
            {
                if (value <= 0)
                {
                    this._weight = 0.1f;
                    return;
                }

                this._weight = value;
            }
        }

        public void Equip()
        {
        }

        public void Tick()
        {
        }
    }
}