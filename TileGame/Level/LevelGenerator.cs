using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using SFML.Graphics;
using SFML.System;
using TileGame.Game;
using TileGame.Interfaces;
using TileGame.Pathfinding;
using TileGame.Tiles;
using TileGame.Utility.Random;

namespace TileGame.Level
{
    public class LevelGenerator
    {
        private readonly GameManager _manager;
        public uint Identifier { get; set; } = 0;
        private LevelTemplate LevelTemplate { get; set; }
        private TileFactory TileFactory { get; set; }
        


        public LevelGenerator(GameManager manager, TileFactory tileFactory)
        {
            _manager = manager;
            TileFactory = tileFactory;
            string[] allowedTiles = new[] { "Grass" };
            string[] allowedBlockers = new[] { "Mountains" };
            var assembly = new TileAssembly(allowedTiles, allowedBlockers);
            LevelTemplate = new LevelTemplate(assembly, new Vector2i(24, 24), new Vector2f(8, 8));
        }

        public Level GenerateLevel(LevelTemplate template, int generatingSpeed)
        {
            var level = new Level(_manager, template.MapSize, generatingSpeed);

            level.TileMatrix = new Tile[template.MapSize.X, template.MapSize.Y];
            level.Pathfinding = new Pathfinding.Pathfinding(level.TileMatrix);
            PlaceMapBarriers(template.MapSize.X, template.MapSize.Y, nameof(Mountains), level);
            PlaceEssentialTiles(template.MapSize.X, template.MapSize.Y, nameof(StartTile), level);
            GenerateRandomTiles(template, level);
            return level;
        }

        private Tile CreateTile(string tileName, int xPos, int yPos)
        {
            var tile = TileFactory.CreateTile(tileName);
            tile.TileRect.Position = new Vector2f(xPos * LevelTemplate.TileSize.X, yPos * LevelTemplate.TileSize.Y);
            tile.TileRect.Size = LevelTemplate.TileSize;
            tile.Node.MatrixPosition = new Vector2i(xPos, yPos);
            tile.Node.WorldPosition = tile.TileRect.Position;

            return tile;
        }

        private bool GenerateChunk(Level level, string tileName, float repeatPercentage)
        {
            var result = level.FindEmptyTiles();
            var centerTile = RandomGenerator.RandomNumber(0, result.Count);
            return false;
        }

        private void CreateSpawnPosition()
        {
        }

        private void CreateEssentialTiles(int mapSizeX, int mapSizeY, string tileName, Level level)
        {
            string[] names = new string[] { "StartTile", "ExitTile" };
            var result = RandomGenerator.RandomNumber(0, 1);
            if (result == 1)
            {
                names[0] = nameof(ExitTile);
                names[1] = nameof(StartTile);
            }

            var number = RandomGenerator.RandomNumber(1, mapSizeX - 2);
            Console.WriteLine(1 + " " + number);
            level.TileMatrix[1, number] = CreateTile(names[0], 1, number);
            level.SpawnTile = level.TileMatrix[1, number];
            Console.WriteLine(level.SpawnTile.Node.MatrixPosition);

            number = RandomGenerator.RandomNumber(1, mapSizeY - 2);
            Console.WriteLine(mapSizeX - 2 + " " + number);
            level.TileMatrix[mapSizeX - 2, number] = CreateTile(names[1], mapSizeX - 2, number);
            level.ExitTile = level.TileMatrix[mapSizeX - 2, number];
            Console.WriteLine(level.ExitTile.Node.MatrixPosition);
        }

        public void PlaceEssentialTiles(int mapSizeX, int mapSizeY, string tileName, Level level)
        {
            CreateEssentialTiles(mapSizeX, mapSizeY, tileName, level);
        }

        private void GenerateRandomTiles(LevelTemplate template, Level level)
        {
            for (int i = 0; i < template.MapSize.X; i++)
            {
                for (int j = 0; j < template.MapSize.Y; j++)
                {
                    var xPos = i;
                    var yPos = j;
                    var result = RandomGenerator.RandomNumber(0, 1);
                    if (result == 0)
                    {
                        level.LevelGenerationQueue.Enqueue(() =>
                        {
                            if (level.CheckTilePlaced(new Vector2i(xPos, yPos)))
                            {
                                level.TileMatrix[xPos, yPos] =
                                    CreateTile(nameof(Mountains), xPos, yPos);
                            }
                        });
                    }
                    else
                    {
                        level.LevelGenerationQueue.Enqueue(() =>
                        {
                            if (level.CheckTilePlaced(new Vector2i(xPos, yPos)))
                            {
                                level.TileMatrix[xPos, yPos] = CreateTile(nameof(Grass), xPos, yPos);
                            }
                        });
                    }
                }
            }
        }

        private void PlaceMapBarriers(int mapSizeX, int mapSizeY, string tileName, Level level)
        {
            for (int i = 0; i < mapSizeY; i++)
            {
                if (level.CheckTilePlaced(new Vector2i(0, i)))
                {
                    level.TileMatrix[0, i] = CreateTile(tileName, 0, i);
                }
            }

            for (int i = 0; i < mapSizeY; i++)
            {
                if (level.CheckTilePlaced(new Vector2i(mapSizeY - 1, i)))
                {
                    level.TileMatrix[mapSizeX - 1, i] = CreateTile(tileName, mapSizeX - 1, i);
                }
            }

            for (int i = 0; i < mapSizeX - 1; i++)
            {
                if (level.CheckTilePlaced(new Vector2i(i, 0)))
                {
                    level.TileMatrix[i, 0] = CreateTile(tileName, i, 0);
                }
            }

            for (int i = 0; i < mapSizeX - 1; i++)
            {
                if (level.CheckTilePlaced(new Vector2i(i, mapSizeY - 1)))
                {
                    level.TileMatrix[i, mapSizeY - 1] = CreateTile(tileName, i, mapSizeY - 1);
                }
            }
        }
    }
}