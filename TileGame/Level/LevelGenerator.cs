
using SFML.System;
using TileGame.Game;
using TileGame.Items;
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
            string[] spawnableItems = new[] { nameof(Weapon), nameof(Armor), nameof(Ring) };
            var tileAssembly = new TileAssembly(allowedTiles, allowedBlockers);
            var itemassembly = new ItemAssembly(spawnableItems);
            LevelTemplate = new LevelTemplate(tileAssembly, new Vector2i(24, 24), new Vector2f(8, 8), itemassembly);
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

        public Level GenerateLevel(LevelTemplate template, int generatingSpeed, TileAssembly assembly)
        {
            var level = new Level(_manager, template.MapSize, generatingSpeed);

            level.TileMatrix = new Tile[template.MapSize.X, template.MapSize.Y];
            level.Pathfinding = new Pathfinding.Pathfinding(level.TileMatrix);
            PlaceMapBarriers(template.MapSize.X, template.MapSize.Y, assembly.BlockadeTiles[0], level);
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

            var number = RandomGenerator.RandomNumber(1, mapSizeY - 2);
            level.TileMatrix[1, number] = CreateTile(names[0], 1, number);
            level.SpawnTile = level.TileMatrix[1, number];

            number = RandomGenerator.RandomNumber(1, mapSizeY - 2);
            level.TileMatrix[mapSizeX - 2, number] = CreateTile(names[1], mapSizeX - 2, number);
            level.ExitTile = level.TileMatrix[mapSizeX - 2, number];
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
                    var result = RandomGenerator.RandomNumber(0, 10);

                    if (result == 0)
                    {
                        level.LevelGenerationQueue.Enqueue(() =>
                        {
                            if (level.CheckTilePlaced(new Vector2i(xPos, yPos)))
                            {
                                var tileIndex =
                                    RandomGenerator.RandomNumber(0, template.TileAssembly.TraversableTiles.Length - 1);

                                level.TileMatrix[xPos, yPos] =
                                    CreateTile(template.TileAssembly.TraversableTiles[tileIndex], xPos, yPos);
                            }
                        });
                    }
                    else if (result == 1 || result == 2)
                    {
                        level.LevelGenerationQueue.Enqueue(() =>
                        {
                            if (level.CheckTilePlaced(new Vector2i(xPos, yPos)))
                            {
                                var tileIndex =
                                    RandomGenerator.RandomNumber(0, template.TileAssembly.BlockadeTiles.Length - 1);

                                level.TileMatrix[xPos, yPos] =
                                    CreateTile(template.TileAssembly.BlockadeTiles[tileIndex], xPos, yPos);
                            }
                        });
                    }
                    else
                    {
                        level.LevelGenerationQueue.Enqueue(() =>
                        {
                            if (level.CheckTilePlaced(new Vector2i(xPos, yPos)))
                            {
                                level.TileMatrix[xPos, yPos] = CreateTile(template.TileAssembly.TraversableTiles[0],
                                    xPos, yPos);
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