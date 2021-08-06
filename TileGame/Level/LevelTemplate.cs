using SFML.System;
using TileGame.Tiles;

namespace TileGame.Level
{
    public class LevelTemplate
    {
        public LevelTemplate(TileAssembly tileAssembly, Vector2u mapSize, Vector2f tileSize)
        {
            TileAssembly = tileAssembly;
            MapSize = mapSize;
            TileSize = tileSize;
        }

        public TileAssembly TileAssembly { get; private set; }
        
        public Vector2u MapSize { get; private set; }
        public Vector2f TileSize { get; private set; }
    }
}