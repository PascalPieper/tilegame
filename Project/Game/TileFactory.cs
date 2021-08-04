using TileGame.Tiles;

namespace TileGame.Game
{
    class TileFactory : ReflectFactory<Tile>
    {
        public override Tile GetInstance(string typeName)
        {
            return base.GetInstance(typeName);
        }
    }
}