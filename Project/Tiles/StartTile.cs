using Project.Tiles.Behavior;
using SFML.Graphics;
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
        }
    }
}