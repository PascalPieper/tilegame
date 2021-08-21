using TileGame.Tiles.Behavior;
using SFML.Graphics;
using TileGame.Game;
using TileGame.Interfaces;

namespace TileGame.Tiles
{
    public class StartTile : Tile, ITraversable
    {
        public void OnEnter()
        {
        }

        public void OnExit()
        {
        }

        public StartTile()
        {
            ResourceManager resourceManager = new ResourceManager();
            BaseTextureName = "resources/spawnpoint.png"; 
            this.TileRect.Texture = resourceManager.LoadTexture(BaseTextureName);
        }
    }
}