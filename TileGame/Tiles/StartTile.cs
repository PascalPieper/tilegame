using TileGame.Tiles.Behavior;
using SFML.Graphics;
using TileGame.Game;
using TileGame.Interfaces;

namespace TileGame.Tiles
{
    public class StartTile : Tile, ITraversable, IOccupied
    {
        public IOccupyTile OccupyingEntity { get; }

        public void OnEnter()
        {
        }

        public void OnExit()
        {
        }

        public StartTile()
        {
            ResourceManager resourceManager = new ResourceManager();
            this.TileRect.Texture = resourceManager.LoadTexture("resources/spawnpoint.png");
        }
    }
}