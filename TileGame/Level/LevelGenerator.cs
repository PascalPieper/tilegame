using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
        public LevelTemplate LevelTemplate { get; set; }
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
            LevelTemplate = new LevelTemplate(assembly, new Vector2u(8, 8), new Vector2f(8, 8));
        }

        public Level Generate(LevelTemplate template)
        {
            var level = new Level(_manager);

            level.TileMatrix = new Tile[template.MapSize.X, template.MapSize.Y];
            PlaceMapBarriers(template.MapSize.X, template.MapSize.Y, nameof(Mountains), level);

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
                                CreateTile(nameof(StartTile), xPos, yPos);
                        });
                    }
                    else
                    {
                        _levelGenerationQueue.Enqueue(() =>
                        {
                            if (level.CheckTilePlaced(new Vector2u(xPos, yPos)))
                            {
                                level.TileMatrix[xPos, yPos] = CreateTile(nameof(Grass), xPos, yPos);
                            }
                        });
                    }
                }
            }

            return level;
        }

        private Tile CreateTile(string tileName, uint xPos, uint yPos)
        {
            var tile = TileFactory.CreateTile(tileName);
            tile.TileRect.Position = new Vector2f(xPos * LevelTemplate.TileSize.X, yPos * LevelTemplate.TileSize.Y);
            tile.TileRect.Size = LevelTemplate.TileSize;

            return tile;
        }

        private void CreateSpawnPosition()
        {
        }


        public void PlaceSpawnTile()
        {
        }


        private void PlaceMapBarriers(uint mapSizeX, uint mapSizeY, string tileName, Level level)
        {
            for (uint i = 0; i < mapSizeY; i++)
            {
                if (level.CheckTilePlaced(new Vector2u(0, i)))
                {
                    level.TileMatrix[0, i] = CreateTile(tileName, 0, i);
                }
            }

            for (uint i = 0; i < mapSizeY - 1; i++)
            {
                if (level.CheckTilePlaced(new Vector2u(mapSizeX - 1, i)))
                {
                    level.TileMatrix[mapSizeX - 1, i] = CreateTile(tileName, mapSizeX - 1, i);
                }
            }

            for (uint i = 0; i < mapSizeX - 1; i++)
            {
                if (level.CheckTilePlaced(new Vector2u(i, 0)))
                {
                    CreateTile(tileName, i, 0);
                }
            }

            for (uint i = 0; i < mapSizeX - 1; i++)
            {
                if (level.CheckTilePlaced(new Vector2u(i, mapSizeY - 1)))
                {
                    CreateTile(tileName, i, mapSizeY - 1);
                }
            }
        }

        public void Tick()
        {
            _levelGenerationQueue.TryDequeue(out var task);
            task?.Invoke();
        }
    }
}