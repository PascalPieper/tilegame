using System;
using System.Numerics;
using TileGame.Tiles.Behavior;
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

        public Grass()
        {
            this.TileRect.FillColor = new Color(55, 200, 75);
            
        }
        
    }
}