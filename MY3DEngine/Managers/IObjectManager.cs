using MY3DEngine.BaseObjects;
using System.Collections.Generic;
using System.ComponentModel;

namespace MY3DEngine.Managers
{
    /// <summary>
    /// Manages the list of objects
    /// </summary>
    public interface IObjectManager
    {
        /// <summary>
        /// List of game objects
        /// </summary>
        BindingList<GameObject> GameObjects { get; }

        /// <summary>
        /// Get the list of game objects
        /// </summary>
        /// <returns></returns>
        IEnumerable<GameObject> GetGameObjects { get; }

        /// <summary>
        /// Add an object to the list
        /// </summary>
        /// <param name="gameObject">The object to be added</param>
        /// <param name="isNewObject"></param>
        /// <returns>True when successful, false otherwise</returns>
        bool AddObject(GameObject gameObject, bool isNewObject = true);

        /// <summary>
        /// Removes an object from the list
        /// </summary>
        /// <returns>True when successful, false otherwise</returns>
        bool RemoveObject(GameObject gameObject);
    }
}