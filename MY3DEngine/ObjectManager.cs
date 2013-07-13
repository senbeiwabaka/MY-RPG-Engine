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

        public List<ObjectClass> GameObjects
        {
            get { return _gameObjects; }
            set
            {
                if (_gameObjects == null)
                {
                    _gameObjects = new List<ObjectClass>();
                }
                _gameObjects = value;
            }
        }

        public ObjectManager()
        {
            GameObjects = new List<ObjectClass>();
        }

        public bool AddObject(ObjectClass obj)
        {
            try
            {
                lock (GameObjects)
                {
                    GameObjects.Add(obj);
                }
            }
            catch (Exception)
            {
                Engine.GameEngine.Exception.Exceptions.Add(new ExceptionData());
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
