using System;
using System.IO;
using System.Reflection;
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
            ResourceManager resourceManager = new ResourceManager();
            TileRect.Texture = resourceManager.LoadTexture("resources/mountains.png");
        }
    }
}