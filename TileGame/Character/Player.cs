namespace TileGame.Character
{
    public class Player : Char
    {
        public int Strength { get; set; }
        public float StrenghMulti { get; private set; } = 1.5f;
        private float MaxWeight { get; set; }
        public float CurrentWeight { get; private set; } = 0;

        void Reconstruct()
        {
            MaxWeight = Strength * StrenghMulti;
        }
    }
}