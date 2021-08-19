using System;
using TileGame.Items;
using TileGame.Tiles;

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
                item.Sprite.Texture = ResourceManager.Instance.LoadTexture("resources/chest.png");
                gameManager.AddGameObjectToLoop(item, item.Sprite);
                return item;
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR in [ItemFactory.cs] - Specified Tile Name: " + itemIdentifier +
                                  " does not exist as derived type - " + e.Message);

                var tile = GetInstance(nameof(Grass));
                gameManager.AddGameObjectToLoop(tile, tile.Sprite);
                return tile;
            }
        }
    }
}