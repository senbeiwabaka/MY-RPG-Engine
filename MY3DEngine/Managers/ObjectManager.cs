using MY3DEngine.BaseObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace MY3DEngine.GeneralManagers
{
    /// <summary>
    /// Manages the list of objects
    /// </summary>
    public class ObjectManager
    {
        private readonly BindingList<GameObject> gameObjects = new BindingList<GameObject>();

        /// <summary>
        /// List of game objects
        /// </summary>
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
                return this.GameObjects.ToList();
            }
        }
    }
}