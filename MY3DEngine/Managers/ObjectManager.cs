namespace MY3DEngine.Managers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using MY3DEngine.BaseObjects;
    using MY3DEngine.Interfaces;

    /// <inherietdoc/>
    public sealed class ObjectManager : IObjectManager
    {
        private readonly BindingList<BaseObject> gameObjects = new BindingList<BaseObject>();

        /// <inherietdoc/>
        ~ObjectManager()
        {
            Dispose(false);
        }

        /// <inherietdoc/>
        public BindingList<BaseObject> GameObjects
        {
            get
            {
                lock (gameObjects)
                {
                    return gameObjects;
                }
            }
        }

        /// <inherietdoc/>
        public bool AddObject(BaseObject gameObject, bool isNewObject = true)
        {
            try
            {
                gameObject.LoadContent(isNewObject);

                lock (GameObjects)
                {
                    GameObjects.Add(gameObject);
                }
            }
            catch (Exception e)
            {
                Engine.GameEngine.Exception.AddException(e);

                return false;
            }

            return true;
        }

        /// <inherietdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <inherietdoc/>
        public IReadOnlyList<BaseObject> GetGameObjects()
        {
            lock (GameObjects)
            {
                return GameObjects.ToList();
            }
        }

        /// <inherietdoc/>
        public bool LoadObjects(IEnumerable<BaseObject> objects)
        {
            foreach (var item in objects)
            {
                if (!AddObject(item, true))
                {
                    return false;
                }
            }
            return true;
        }

        /// <inherietdoc/>
        public bool RemoveObject(BaseObject gameObject)
        {
            try
            {
                lock (GameObjects)
                {
                    var removed = GameObjects.Remove(gameObject);

                    if (removed)
                    {
                        gameObject?.Dispose();
                        gameObject = null;
                    }

                    return removed;
                }
            }
            catch (Exception e)
            {
                Engine.GameEngine.Exception.AddException(e);

                return false;
            }
        }

        private void Dispose(bool dispose)
        {
            if (dispose)
            {
                foreach (var baseObject in gameObjects)
                {
                    baseObject?.Dispose();
                }
            }
        }
    }
}
