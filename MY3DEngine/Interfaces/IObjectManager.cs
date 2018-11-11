namespace MY3DEngine.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using MY3DEngine.BaseObjects;

    /// <summary>
    /// Manages the list of objects
    /// </summary>
    public interface IObjectManager : IDisposable
    {
        /// <summary>
        /// List of game objects
        /// </summary>
        BindingList<BaseObject> GameObjects { get; }

        /// <summary>
        /// Add an object to the list
        /// </summary>
        /// <param name="gameObject">The object to be added</param>
        /// <param name="isNewObject"></param>
        /// <returns>True when successful, false otherwise</returns>
        bool AddObject(BaseObject gameObject, bool isNewObject = true);

        /// <summary>
        /// Get the list of game objects
        /// </summary>
        /// <returns></returns>
        IReadOnlyList<BaseObject> GetGameObjects();

        /// <summary>
        /// Load a group of objects. Usually used when loading up a game
        /// </summary>
        /// <param name="objects"></param>
        /// <returns></returns>
        bool LoadObjects(IEnumerable<BaseObject> objects);

        /// <summary>
        /// Removes an object from the list
        /// </summary>
        /// <returns>True when successful, false otherwise</returns>
        bool RemoveObject(BaseObject gameObject);
    }
}
