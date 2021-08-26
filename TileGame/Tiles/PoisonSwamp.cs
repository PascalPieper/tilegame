using SFML.Graphics;
using TileGame.Game;
using TileGame.Interfaces;

namespace TileGame.Tiles
{
    public class PoisonSwamp : Tile, ITraversable
    {
        public void OnEnter()
        {
            this.HighlightRect.FillColor = Color.Transparent;
        }

        public void OnExit()
        {
        }

        public PoisonSwamp()
        {
            ResourceManager resourceManager = new ResourceManager();
            this.TileRect.Texture = resourceManager.LoadTexture("resources/poison.png");
            Node.Walkable = true;
        }
    }
}