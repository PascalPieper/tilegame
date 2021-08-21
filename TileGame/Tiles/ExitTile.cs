using TileGame.Game;
using TileGame.Interfaces;

namespace TileGame.Tiles
{
    public class ExitTile : Tile, ITraversable
    {
        public ExitTile()
        {
            ResourceManager resourceManager = new ResourceManager();
            this.TileRect.Texture = resourceManager.LoadTexture("resources/exitpoint.png");
        }

        public void OnEnter()
        {
            throw new System.NotImplementedException();
        }

        public void OnExit()
        {
            throw new System.NotImplementedException();
        }
        
    }
}