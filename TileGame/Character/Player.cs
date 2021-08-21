using SFML.Graphics;
using SFML.System;
using TileGame.Game;
using TileGame.Interfaces;
using TileGame.Level;

namespace TileGame.Character
{
    public class Player : Char, IMove
    {
        public bool CanMove { get; } = true;
        public bool MoveUp()
        {
            this.Sprite.Position += new Vector2f(0, -8);
            return true;
        }

        public bool MoveDown()
        {
            this.Sprite.Position += new Vector2f(0, 8);
            return true;
        }

        public bool MoveLeft()
        {
            this.Sprite.Position += new Vector2f(-8, 0);
            return true;
        }

        public bool MoveRight()
        {
            this.Sprite.Position += new Vector2f(8, 0);
            return true;
        }

        public Player(ItemInventory itemInventory) : base(itemInventory)
        {
            this.Sprite = new Sprite();
            this.Sprite.Texture = ResourceManager.Instance.LoadTexture("resources/player.png");
            Sprite.Scale = new Vector2f(0.65f, 0.65f);
        }

        public override void Update()
        {
            base.Update();
        }
    }
}