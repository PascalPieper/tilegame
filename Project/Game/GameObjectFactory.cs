using System;
using SFML.Graphics;
using SFML.System;
using TileGame.Interfaces;

namespace TileGame.Game
{
    public class GameObjectFactory
    {
        public GameObjectFactory()
        {
            //OnTickEntityCreation = new EventHandler<ITick>();

        }

        public event EventHandler<ITick> OnTickEntityCreation;
        public event EventHandler<Drawable> OnDrawableEntityCreation;

        public T CreateGameObject<T>(Vector2f spawnPosition) where T : Transformable, ITick, Drawable, new()
        {
            var newEntity = new T();
            newEntity.Position = spawnPosition;
            this.OnTickEntityCreation?.Invoke(this, newEntity);
            this.OnDrawableEntityCreation?.Invoke(this, newEntity);
            return newEntity;
        }

        public void CreateAndBroadcastEntity<T>() where T : Transformable, ITick, Drawable, new()
        {
            // OnTickEntityCreation.Invoke(this, something);
            // OnDrawableEntityCreation.Invoke(this, something);
        }
    }
}