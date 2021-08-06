namespace TileGame.Interfaces
{
    public interface ITick
    {
        void Tick();
        uint Identifier { get; }
    }
}