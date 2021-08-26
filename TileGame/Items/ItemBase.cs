using SFML.Graphics;
using TileGame.Interfaces;

namespace TileGame.Items
{
    public class ItemBase : ITick
    {
        public uint Identifier { get; } = 0;
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public Sprite Sprite { get; set; } = null;

        public int StrengthBonus = 0;

        private float _weight;

        public float Weight
        {
            get => this._weight;
            set
            {
                if (value <= 0)
                {
                    this._weight = 0.1f;
                    return;
                }

                this._weight = value;
            }
        }

        public ItemBase(string name, string description, double price, float weight, string textureName, int strengthBonus)
        {
            Name = name;
            Description = description;
            Price = price;
            _weight = weight;
            StrengthBonus = strengthBonus;
        }

        public ItemBase()
        {
            
        }


        public void Equip()
        {
        }

        public void Tick()
        {
        }
    }
}