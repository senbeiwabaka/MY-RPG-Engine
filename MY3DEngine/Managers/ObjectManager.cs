using MY3DEngine.BaseObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace MY3DEngine.Managers
{
    /// <inherietdoc/>
    public class ObjectManager : IObjectManager
    {
        private readonly BindingList<GameObject> gameObjects = new BindingList<GameObject>();

        /// <inherietdoc/>
        public BindingList<GameObject> GameObjects
        {
            get
            {
                lock (this.gameObjects)
                {
                    return this.gameObjects;
                }
            }
        }

        /// <inherietdoc/>
        public IEnumerable<GameObject> GetGameObjects
        {
            get
            {
                lock (this.GameObjects)
                {
                    return this.GameObjects.ToList();
                }
            }
        }

        /// <inherietdoc/>
        public bool AddObject(GameObject gameObject, bool isNewObject = true)
        {
            try
            {
                gameObject.LoadContent(isNewObject);

                lock (this.GameObjects)
                {
                    this.GameObjects.Add(gameObject);
                }
            }
            catch (Exception e)
            {
                Engine.GameEngine.AddException(e);

                return false;
            }

            return true;
        }

        /// <inherietdoc/>
        public bool RemoveObject(GameObject gameObject)
        {
            try
            {
                lock (this.GameObjects)
                {
                    var removed = this.GameObjects.Remove(gameObject);

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
                Engine.GameEngine.AddException(e);

                return false;
            }
        }
    }
}