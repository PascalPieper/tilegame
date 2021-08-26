using System.Resources;
using SFML.Graphics;
using ResourceManager = TileGame.Game.ResourceManager;

namespace TileGame.Items
{
    class Ring : ItemBase
    {

        public Ring()
        {
            Name = "Ring";
            Sprite = new Sprite();
            this.Sprite.Texture = ResourceManager.Instance.LoadTexture("resources/exitpoint.png");
        }
    }
}