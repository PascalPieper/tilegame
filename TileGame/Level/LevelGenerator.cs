using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using TileGame.Game;
using TileGame.Interfaces;
using TileGame.Tiles;
using TileGame.Utility.Random;

namespace TileGame.Level
{
    public class LevelGenerator : ITick
    {
        private readonly GameManager _manager;
        public uint Identifier { get; set; } = 0;
        public LevelTemplate Template { get; set; }
        public TileFactory TileFactory { get; set; }

        private delegate void LevelTask();

        private readonly Queue<LevelTask> _levelGenerationQueue = new Queue<LevelTask>();


        public LevelGenerator(GameManager manager, TileFactory tileFactory)
        {
            _manager = manager;
            TileFactory = tileFactory;
            string[] allowedTiles = new[] { "Grass" };
            string[] allowedBlockers = new[] { "Mountains" };
            var assembly = new TileAssembly(allowedTiles, allowedBlockers);
            Template = new LevelTemplate(assembly, new Vector2u(15,15), new Vector2f(15,15));
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

        public void PlaceMapBarriers(uint mapSizeX, uint mapSizeY, string tileName)
        {
            for (int i = 0; i < mapSizeX; i++)
            {
                // Generate {0 - 1,2,3,4}
                // Generate {max - 1,2,3,4}
                
                // Generate {1,2,3,4 - 0)
                // Generate {1,2,3,4 - max}
            }
        }

        public void Tick()
        {
            _levelGenerationQueue.TryDequeue(out var task);
            task?.Invoke();
        }
    }
}