using System.Collections.Generic;
using System.Data;
using SFML.Graphics;
using TileGame.Interfaces;
using TileGame.Tiles;

namespace TileGame.Game
{
    public class Game : IStart
    {
        public List<ITick> tickingGameObjects;
        public List<Drawable> DrawableGameObjects;
        public void Start()
        {
        }

        public void Run()
        {
            Input();
            Update();
            Draw();
        }

        private void Draw()
        {
            throw new System.NotImplementedException();

        }

        private void Update()
        {
            throw new System.NotImplementedException();

        }

        private void Input()
        {

        }

    }
}