using System;
using System.Numerics;
using TileGame.Tiles.Behavior;
using SFML.Graphics;
using TileGame.Game;
using TileGame.Interfaces;
using Char = TileGame.Character.Char;

namespace TileGame.Tiles
{
    class Grass : Tile, ITraversable, IOccupiable
    {
        public void OnEnter()
        {
            this.HighlightRect.FillColor = Color.Transparent;
        }

        public void OnExit()
        {
        }

        public Grass()
        {
            ResourceManager resourceManager = new ResourceManager();
            this.TileRect.Texture = resourceManager.LoadTexture("resources/grass.png");
            Node.Walkable = true;
        }

        public bool Occupied { get; set; } = false;
        public void OnOccupy()
        {
        }
    }

    internal interface IOccupiable
    {
        bool Occupied { get; }
        void OnOccupy();
    }
}