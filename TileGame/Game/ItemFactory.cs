using System;
using TileGame.Items;
using TileGame.Tiles;
using TileGame.Utility.Random;

namespace TileGame.Game
{
    class ItemFactory : ReflectFactory<ItemBase>
    {
        public GameManager gameManager;

        public ItemFactory(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        public ItemBase CreateItem(string itemIdentifier)
        {
            try
            {
                var item = GetInstance(itemIdentifier);

                item.Price = RandomGenerator.RandomNumber(0, 255);
                item.Weight = RandomGenerator.RandomNumber(0.1f, 5);
                return item;
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR in [ItemFactory.cs] - Specified Tile Name: " + itemIdentifier +
                                  " does not exist as derived type - " + e.Message);

                var item = GetInstance(nameof(Ring));
                //gameManager.AddGameObjectToLoop(item, item.Sprite);
                return item;
            }
        }
    }
}