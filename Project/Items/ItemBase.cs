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


        private float weight;

        public float Weight
        {
            get => this.weight;
            private set
            {
                if (value <= 0)
                {
                    this.weight = 0;
                    return;
                }

                this.weight = value;
            }
        }


    }
}