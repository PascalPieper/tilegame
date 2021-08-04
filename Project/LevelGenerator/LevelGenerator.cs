using System.Collections.Generic;
using Project.Interfaces;
using SFML.Graphics;
using SFML.System;
using TileGame.Game;
using TileGame.Interfaces;
using TileGame.Tiles;

namespace Project.LevelGenerator
{
    public class LevelGenerator
    {
        public LevelGenerator( GameManager gameManager, Vector2u mapSize, Vector2f tileSize)
        {
            _manager = gameManager;
            MapSize = mapSize;
            TileSize = tileSize;
            tileMatrix = new Tile[mapSize.X, mapSize.Y];
        }

        private GameManager _manager;

        public Vector2u MapSize { get; }
        public Vector2f TileSize { get; }
        public Tile[,] tileMatrix;
        public ITraversable[] AllowedTraversableTiles { get; }
        public IBlockade[] AllowedBlockadeTiles { get; }

        public void Generate()
        {
            var factory = new TileFactory();
            for (var i = 0; i < MapSize.X; i++)
            {
                for (var j = 0; j < MapSize.Y; j++)
                {
                    var temp = factory.GetInstance("Grass");
                    temp.TileRect.Position = new Vector2f(i, j);
                    temp.TileRect.Size = TileSize;
                    AddToGameManager(temp.TileRect, temp);

                    tileMatrix[i,j] = temp;
                }
            }
        }

        private void AddToGameManager(Drawable drawable, ITick tick)
        {
            _manager.Drawables.Add(drawable);
            _manager.Entities.Add(tick);
        }
    }
}