namespace TileGame.Items
{
    public interface IWeight
    {
        float Weight { get; }
    }

    public interface IDescribed
    {
        string Name { get; }
        string Description { get; }
    }

    public interface ITradeable
    {
    }

    public class ItemBase : IWeight, IDescribed, ITradeable
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public double Price { get; private set; }

        public ItemBase(string name, string description, double price, float weight)
        {
            Name = name;
            Description = description;
            Price = price;
            _weight = weight;
        }

        private float _weight;

        public float Weight
        {
            get => this._weight;
            private set
            {
                if (value <= 0)
                {
                    this._weight = 0;
                    return;
                }

                this._weight = value;
            }
        }
    }
}