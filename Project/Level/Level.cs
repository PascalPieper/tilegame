using System.Collections.Generic;
using SFML.System;
using TileGame.Game;
using TileGame.Tiles;

namespace Project.Level
{
    public class Level
    {
        public Tile[,] TileMatrix { get; set; }
        public List<Vector2i> EmptyTiles { get; set; }
        private GameManager _gameManager;

        public Level(GameManager gameManager)
        {
            this._gameManager = gameManager;
        }

        public void UnloadLevel()
        {
            for (var i = 0; i < TileMatrix.GetLength(0); i++)
            {
                for (var j = 0; j < TileMatrix.GetLength(1); j++)
                {
                    TileMatrix[i, j] = null;
                }
            }
            _gameManager.UnloadAllGameObjects();
        }
    }
}