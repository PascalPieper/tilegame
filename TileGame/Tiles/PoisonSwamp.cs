using TileGame.Game;

namespace TileGame.Tiles
{
    public class PoisonSwamp : Tile
    {
        public void OnEnter()
        {
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