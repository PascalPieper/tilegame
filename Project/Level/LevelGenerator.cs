using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using TileGame.Game;
using TileGame.Interfaces;
using TileGame.Tiles;

namespace Project.Level
{
    public class LevelGenerator
    {
        private readonly GameManager _manager;

        public LevelGenerator(GameManager manager)
        {
            _manager = manager;
        }

        public Level Generate(LevelTemplate template)
        {
            var level = new Level(_manager);
            TileFactory factory = new TileFactory(_manager);

            level.TileMatrix = new Tile[template.MapSize.X, template.MapSize.Y];

            for (var i = 0; i < template.MapSize.X; i++)
            {
                for (var j = 0; j < template.MapSize.Y; j++)
                {
                    var temp = factory.CreateTile(nameof(Grass));
                    temp.TileRect.Position = new Vector2f(i * template.TileSize.X, j * template.TileSize.Y);
                    temp.TileRect.Size = template.TileSize;
                    AddToGameManager(temp.TileRect, temp);

                    level.TileMatrix[i, j] = temp;
                }
            }

            var start = factory.CreateTile(nameof(StartTile));
            start.TileRect.Size = template.TileSize;
            start.TileRect.Position = new Vector2f(0, 0);
            AddToGameManager(start.TileRect, start);


            return level;
        }

        private void AddToGameManager(Drawable drawable, ITick tick)
        {
            _manager.Drawables.Add(drawable);
            _manager.Entities.Add(tick);
        }
    }
}