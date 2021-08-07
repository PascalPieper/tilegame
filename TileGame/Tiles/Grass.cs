using System;
using System.Numerics;
using TileGame.Tiles.Behavior;
using SFML.Graphics;
using TileGame.Game;
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
            ResourceManager resourceManager = new ResourceManager();
            this.TileRect.Texture = resourceManager.LoadTexture("resources/grass.png");
        }
        
    }
}