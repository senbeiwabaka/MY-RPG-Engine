using System;
using System.ComponentModel;

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
        /// <returns>True when successful, false otherwise</returns>
        public bool AddObject(GameObject gameObject)
        {
            try
            {
                lock (GameObjects)
                {
                    gameObject.LoadContent();

                    this.GameObjects.Add(gameObject);
                }
            }
            catch (Exception e)
            {
                Engine.GameEngine.Exception.Exceptions.Add(new ExceptionData(e.Message, e.Source, e.StackTrace));
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
                Engine.GameEngine.Exception.Exceptions.Add(new ExceptionData(e.Message, e.Source, e.StackTrace));
                return false;
            }
        }
    }
}