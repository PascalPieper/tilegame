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
        }

        public Grass(string name, CharacterEffectBehavior behavior) : base(name, behavior)
        {
            this.TileRect.FillColor = Color.Green;
        }
    }
}