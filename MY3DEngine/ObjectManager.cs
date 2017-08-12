using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace MY3DEngine
{
    /// <summary>
    /// Manages the list of objects
    /// </summary>
    public class ObjectManager
    {
        private List<GameObject> _gameObjects = null;
        private int _id;
        private int _index;

        public List<GameObject> GameObjects
        {
            get
            {
                if (_gameObjects == null)
                {
                    _gameObjects = new List<GameObject>();
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
                    _gameObjects = new List<GameObject>();
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
            GameObjects = new List<GameObject>();
            _index = -1;
            _id = -1;
        }

        /// <summary>
        /// Add an object to the list
        /// </summary>
        /// <param name="obj">The object to be added</param>
        /// <returns>True when successful, false otherwise</returns>
        public bool AddObject(GameObject obj)
        {
            try
            {
                lock (GameObjects)
                {
                    //if (obj.GetType() == typeof(LightClass))
                    //{
                    //    ((LightClass)obj).index = ++_index;
                    //}
                    GameObjects.Add(obj);
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
        public bool RemoveObject()
        {
            try
            {
                lock (GameObjects)
                {
                }
                Engine.GameEngine.Exception.Exceptions.Add(new ExceptionData("Not Implemented", "ObjectManager.RemoveObject", string.Empty));
            }
            catch (Exception e)
            {
                Engine.GameEngine.Exception.Exceptions.Add(new ExceptionData(e.Message, e.Source, e.StackTrace));
                return false;
            }

            return true;
        }
    }
}