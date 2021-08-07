using SFML.Graphics;
using SFML.Graphics.Glsl;
using TileGame.Game;
using TileGame.Tiles;

namespace TileGame.Tiles
{
    class Mountains : Tile
    {
        public Mountains()
        {
            
            this.TileRect.FillColor = new Color(155,155,155);
            // ResourceManager resourceManager = new ResourceManager();
            // resourceManager.LoadTextureFromFile("test", "TileGame/resources/a.png");
            // this.TileRect.Texture = new Texture("TileGame/resources/a.png");
        }
    }
}