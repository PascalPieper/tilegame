using System.Collections.Generic;
using System.Linq;
using SFML.Graphics;
using TileGame.Interfaces;

namespace TileGame.Game
{
    public class GameManager

    {
        private readonly Dictionary<uint, ITick> _tickingGameObjects;
        private readonly Dictionary<uint, Drawable> _drawableGameObjects;
        private readonly Dictionary<uint, IUpdate> _updatingGameObjects;

        private uint IdCount { get; set; } = 0;

        public GameManager()
        {
            _tickingGameObjects = new Dictionary<uint, ITick>();
            _drawableGameObjects = new Dictionary<uint, Drawable>();
            _updatingGameObjects = new Dictionary<uint, IUpdate>();
        }

        public void Tick()
        {
            for (int index = _tickingGameObjects.Count; index > 0; index--)
            {
                var item = _tickingGameObjects.ElementAt(index);
                item.Value.Tick();
            }
        }

        public void Draw(RenderWindow window)
        {
            foreach (var entity in this._drawableGameObjects)
            {
                window.Draw(entity.Value);
            }
        }

        public void Update()
        {
            for (int index = _updatingGameObjects.Count; index > 0; index--)
            {
                var item = _updatingGameObjects.ElementAt(index - 1);
                item.Value.Update();
            }
        }

        public void AddGameObjectToLoop(ITick tickingGo, Drawable drawableGo)
        {
            _tickingGameObjects.Add(IdCount, tickingGo);
            _drawableGameObjects.Add(IdCount, drawableGo);
            IdCount++;
        }
        public void AddGameObjectToLoop(ITick tickingGo, Drawable drawableGo, IUpdate updateableGo)
        {
            _tickingGameObjects.Add(IdCount, tickingGo);
            _drawableGameObjects.Add(IdCount, drawableGo);
            _updatingGameObjects.Add(IdCount, updateableGo);
            IdCount++;
        }
        public void UnloadAllGameObjects()
        {
            foreach(KeyValuePair<uint, ITick> entry in _tickingGameObjects)
            {
                _tickingGameObjects.Remove(entry.Key);
            }
            foreach(KeyValuePair<uint, Drawable> entry in _drawableGameObjects)
            {
                _drawableGameObjects.Remove(entry.Key);
            }
            foreach (KeyValuePair<uint, IUpdate> entry in _updatingGameObjects)
            {
                _updatingGameObjects.Remove(entry.Key);
            }
        }
    }
}