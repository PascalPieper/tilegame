using System;
using System.Collections.Generic;
using Project.Game;
using Project.Utility.Random;
using SFML.Graphics;
using SFML.System;
using TileGame.Game;
using TileGame.Interfaces;
using TileGame.Tiles;

namespace Project.Level
{
    public class LevelGenerator : ITick
    {
        private readonly GameManager _manager;
        public uint Identifier { get; set; } = 0;

        private delegate void LevelTask();

        private readonly Queue<LevelTask> _levelGenerationQueue = new Queue<LevelTask>();

        public LevelGenerator(GameManager manager)
        {
            _manager = manager;
        }

        public Level Generate(LevelTemplate template)
        {
            var level = new Level(_manager);
            TileFactory factory = new TileFactory(_manager);

            level.TileMatrix = new Tile[template.MapSize.X, template.MapSize.Y];

            for (uint i = 0; i < template.MapSize.X; i++)
            {
                for (uint j = 0; j < template.MapSize.Y; j++)
                {
                    var xPos = i;
                    var yPos = j;
                    var result = RandomGenerator.RandomNumber(0, 1);
                    if (result == 0)
                    {
                        _levelGenerationQueue.Enqueue(() =>
                        {
                            level.TileMatrix[xPos, yPos] =
                                CreateTile(factory, nameof(StartTile), template, xPos, yPos);
                        });
                    }
                    else
                    {
                        _levelGenerationQueue.Enqueue(() =>
                        {
                            level.TileMatrix[xPos, yPos] = CreateTile(factory, nameof(Grass), template, xPos, yPos);
                        });
                        
                    }

 
                }
            }

            return level;
        }

        private void CreateSpawnPosition()
        {
        }

        private static Tile CreateTile(TileFactory factory, string tileName, LevelTemplate template, uint xPos,
            uint yPos)
        {
            var tile = factory.CreateTile(tileName);
            tile.TileRect.Position = new Vector2f(xPos * template.TileSize.X, yPos * template.TileSize.Y);
            tile.TileRect.Size = template.TileSize;

            return tile;
        }

        public void PlaceSpawnTile()
        {
            
        }

        public void PlaceMapBarriers()
        {
            
        }

        public void Tick()
        {
            _levelGenerationQueue.TryDequeue(out var task);
            task?.Invoke();
        }
    }
}