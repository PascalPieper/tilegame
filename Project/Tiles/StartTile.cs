using System;
using System.Numerics;
using Project.Tiles.Behavior;
using SFML.Graphics;
using TileGame.Interfaces;
using Char = TileGame.Character.Char;

namespace TileGame.Tiles
{
    public class StartTile : Tile, ITraversable, IOccupied
    {
        public IOccupyTile OccupyingEntity { get; }
        public void OnEnter()
        {
            
            if (OccupyingEntity.GetType() == typeof(Char))
            {
                Behavior.TraverseEffect(OccupyingEntity as Char);
            }
            
        }

        public void OnExit()
        {
        }


        public StartTile(string name, RectangleShape tileRect, Vector<uint> currentMapPosition, AdjacentTiles adjacentTiles, CharacterEffectBehavior behavior) : base(name, tileRect, currentMapPosition, adjacentTiles, behavior)
        {
        }
    }
}