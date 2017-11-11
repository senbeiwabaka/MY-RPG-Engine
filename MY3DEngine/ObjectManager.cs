using MY3DEngine.BaseObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace MY3DEngine
{
    /// <summary>
    /// Manages the list of objects
    /// </summary>
    public class ObjectManager
    {
        private BindingList<GameObject> _gameObjects = null;

        public BindingList<GameObject> GameObjects
        {
            get
            {
                if (_gameObjects == null)
                {
                    _gameObjects = new BindingList<GameObject>();
                }

                lock (_gameObjects)
                {
                    return _gameObjects;
                }
            }
            set
            {
                if (_gameObjects == null)
                {
                    _gameObjects = new BindingList<GameObject>();
                }

                lock (_gameObjects)
                {
                    _gameObjects = value;
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ObjectManager()
        {
            this.GameObjects = new BindingList<GameObject>();
        }

        /// <summary>
        /// Add an object to the list
        /// </summary>
        /// <param name="gameObject">The object to be added</param>
        /// <param name="isNewObject"></param>
        /// <returns>True when successful, false otherwise</returns>
        public bool AddObject(GameObject gameObject, bool isNewObject = true)
        {
            try
            {
                lock (this.GameObjects)
                {
                    gameObject.LoadContent(isNewObject);

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

        /// <summary>
        /// Removes an object from the list
        /// </summary>
        /// <returns>True when successful, false otherwise</returns>
        public bool RemoveObject(GameObject gameObject)
        {
            try
            {
                lock (this.GameObjects)
                {
                    return this.GameObjects.Remove(gameObject);
                }
            }
            catch (Exception e)
            {
                Engine.GameEngine.AddException(e);

                return false;
            }
        }

        /// <summary>
        /// Get the list of game objects
        /// </summary>
        /// <returns></returns>
        internal IEnumerable<GameObject> GetGameObjects()
        {
            lock (this.GameObjects)
            {
                var objects = this.GameObjects.ToList();
                return objects;
            }
        }
    }
}