using System.Collections.Generic;
using System.Linq;
using SFML.Graphics;
using TileGame.Interfaces;

namespace TileGame.Game
{
    public class GameManager

    {
        public Dictionary<uint, ITick> GameObjects;
        public Dictionary<uint, Drawable> Drawables;

        public uint IdCount { get; private set; } = 0;

        public GameManager()
        {
            GameObjects = new Dictionary<uint, ITick>();
            Drawables = new Dictionary<uint, Drawable>();
        }

        public void Tick()
        {
            for (int index = GameObjects.Count; index > 0; index--)
            {
                var item = GameObjects.ElementAt(index);
                item.Value.Tick();
            }
        }

        public void Draw(RenderWindow window)
        {
            foreach (var entity in this.Drawables)
            {
                window.Draw(entity.Value);
            }
        }

        public void AddGameObjectToLoop(ITick tickingGo, Drawable drawableGo)
        {
            
            GameObjects.Add(IdCount, tickingGo);
            Drawables.Add(IdCount, drawableGo);
            IdCount++;
        }

        public void UnloadAllGameObjects()
        {
            foreach(KeyValuePair<uint, ITick> entry in GameObjects)
            {
                GameObjects.Remove(entry.Key);
            }
            foreach(KeyValuePair<uint, Drawable> entry in Drawables)
            {
                Drawables.Remove(entry.Key);
            }
        }
    }
}