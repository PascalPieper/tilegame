﻿using System;
using TileGame.Tiles;

namespace TileGame.Game
{
    class TileFactory : ReflectFactory<Tile>
    {
        public GameManager gameManager;

        public TileFactory(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        public Tile CreateTile(string tileIdentifier)
        {
            try
            {
                var tile = GetInstance(tileIdentifier);
                gameManager.AddGameObjectToLoop(tile, tile.TileRect);
                return tile;
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR in [TileFactory] - Specified Tile Name: " + tileIdentifier +
                                  " does not exist as derived type");
                
                var tile = GetInstance(nameof(Grass));
                gameManager.AddGameObjectToLoop(tile, tile.TileRect);
                return null;
            }
        }
    }
}