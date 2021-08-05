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
            for (int i = Entities.Count; i > 0; i--)
            {
                Entities[i - 1].Tick();
            }
        }

        public void Draw(RenderWindow window)
        {
            foreach (var entity in this.Drawables)
            {
                window.Draw(entity);
            }
        }

        public void AddGameObjectToLoop(ITick tickingGo, Drawable drawableGo)
        {
            Entities.Add(tickingGo);
            Drawables.Add(drawableGo);
        }

        public void UnloadAllGameObjects()
        {
            for (int i = Entities.Count; i > 0; i--)
            {
                Entities.Remove(Entities[i - 1]);
            }
            for (int i = Drawables.Count ; i > 0; i--)
            {
                Drawables.Remove(Drawables[i - 1]);
            }
        }
    }
}