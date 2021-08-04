using System;
using System.Numerics;
using Project.Tiles.Behavior;
using SFML.Graphics;
using TileGame.Interfaces;
using Char = TileGame.Character.Char;

namespace TileGame.Tiles
{
    class Grass : Tile, ITraversable
    {
        public void OnEnter()
        {
        }

        public void OnExit()
        {
            throw new NotImplementedException();
        }

        public Grass()
        {
        }

        public Grass(string name, RectangleShape tileRect, Vector<uint> currentMapPosition, AdjacentTiles adjacentTiles,
            CharacterEffectBehavior behavior) : base(name, tileRect, currentMapPosition, adjacentTiles, behavior)
        {
            TileRect.FillColor = Color.Green;
        }
    }
}