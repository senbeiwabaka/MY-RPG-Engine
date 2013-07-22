using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace MY3DEngine
{
    public class ObjectManager
    {
        private List<ObjectClass> _gameObjects = null;
        private int index;

        public List<ObjectClass> GameObjects
        {
            get
            {
                if (_gameObjects == null)
                {
                    _gameObjects = new List<ObjectClass>();
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
                    _gameObjects = new List<ObjectClass>();
                }
                lock (_gameObjects)
                {
                    _gameObjects = value;
                }
            }
        }

        public ObjectManager()
        {
            GameObjects = new List<ObjectClass>();
            index = 0;
        }

        public bool AddObject(ObjectClass obj)
        {
            try
            {
                lock (GameObjects)
                {
                    GameObjects.Add(obj);
                    if (obj.GetType() == typeof(LightClass))
                    {
                        ((LightClass)obj).index = index;
                        ++index;
                    }
                }
            }
            catch (Exception e)
            {
                Engine.GameEngine.Exception.Exceptions.Add(new ExceptionData(e.Message, e.Source, e.StackTrace));
                return false;
            }

            return true;
        }

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