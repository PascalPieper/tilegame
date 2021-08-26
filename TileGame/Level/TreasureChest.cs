using SFML.Graphics;
using SFML.System;
using TileGame.Character;
using TileGame.Game;
using TileGame.Items;

namespace TileGame.Level
{
    public class TreasureChest : IOccupy
    {
        public bool IsUsed { get; private set; } = false;
        public Sprite Sprite { get; set; }

        public ItemBase HoldItem { get; set; } = null;

        public TreasureChest()
        {
            this.Sprite = new Sprite();
            this.Sprite.Texture = ResourceManager.Instance.LoadTexture("resources/chest.png");
            Sprite.Scale = new Vector2f(0.65f, 0.65f);
        }


        public ItemBase Open()
        {
            IsUsed = true;
            this.Sprite.Texture = ResourceManager.Instance.LoadTexture("resources/chest_open.png");
            return HoldItem;
        }

        public void GiveItem(Player player)
        {
            player.ItemInventory.AddItemToFront(HoldItem);
        }
    }

    public interface IOccupy
    {
        void GiveItem(Player player);
    }
}