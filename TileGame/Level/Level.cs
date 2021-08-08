﻿using System;
using System.Collections.Generic;
using SFML.System;
using TileGame.Game;
using TileGame.Interfaces;
using TileGame.Tiles;

namespace TileGame.Level
{
    public class Level : ITick, IUpdate
    {
        public Tile[,] TileMatrix { get; set; }
        public List<Vector2u> EmptyTiles { get; set; }
        public Vector2u LevelSize { get; set; }
        private readonly GameManager _gameManager;

        public delegate void LevelTask();

        public readonly Queue<LevelTask> LevelGenerationQueue = new Queue<LevelTask>();
        private int LevelQueueCreationSpeed { get; set; }
        public uint Identifier { get; set; } = 0;


        public Level(GameManager gameManager, Vector2u levelSize)
        {
            this._gameManager = gameManager;
            LevelSize = levelSize;
            LevelQueueCreationSpeed = 10;
        }

        public Level(GameManager gameManager, Vector2u levelSize, int levelQueueCreationSpeed)
        {
            this._gameManager = gameManager;
            LevelSize = levelSize;
            LevelQueueCreationSpeed = levelQueueCreationSpeed;
        }
        
        public void DestroyAllTiles()
        {
            try
            {
                if (this.TileMatrix == null)
                {
                    return;
                }

                for (var i = 0; i < TileMatrix.GetLength(0); i++)
                {
                    for (var j = 0; j < TileMatrix.GetLength(1); j++)
                    {
                        TileMatrix[i, j] = null;
                    }
                }

                _gameManager.UnloadAllGameObjects();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error at [Level.cs] - The level contents are corrupted and cannot be unloaded " +
                                  e.Message);
            }
        }

        public bool CheckTilePlaced(Vector2u tilePosition)
        {
            return TileMatrix[tilePosition.X, tilePosition.Y] == null;
        }

        public List<Vector2u> FindEmptyTiles()
        {
            var empty = new List<Vector2u>();
            for (uint i = 0; i < this.TileMatrix.GetLength(0); i++)
            {
                for (uint j = 0; j < this.TileMatrix.GetLength(1); j++)
                {
                    var tempPosition = new Vector2u(i, j);
                    if (CheckTilePlaced(tempPosition))
                    {
                        empty.Add(tempPosition);
                    }
                }
            }

            this.EmptyTiles = empty;
            return empty;
        }
        
        public void Tick()
        {
        }

        public void Update()
        {
            for (int i = 0; i < LevelQueueCreationSpeed; i++)
            {
                LevelGenerationQueue.TryDequeue(out var task);
                task?.Invoke();
            }
        }
    }
}