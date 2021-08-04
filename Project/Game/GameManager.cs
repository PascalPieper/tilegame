using System.Collections.Generic;
using SFML.Graphics;
using TileGame.Interfaces;

namespace TileGame.Game
{
    public class GameManager

    {
        public List<ITick> Entities;
        public List<Drawable> Drawables;

        public GameManager()
        {
            Entities = new List<ITick>();
            Drawables = new List<Drawable>();
        }

        public void Tick()
        {
            foreach (var entity in this.Entities)
            {
                entity.Tick();
            }
        }

        public void Draw(RenderWindow window)
        {
            foreach (var entity in this.Drawables)
            {
                window.Draw(entity);
            }
        }
    }
}