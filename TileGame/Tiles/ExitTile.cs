using SFML.Graphics;
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
            this.HighlightRect.FillColor = Color.Transparent;
        }

        public void OnExit()
        {
        }
        
    }
}