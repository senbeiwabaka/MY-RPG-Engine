// <copyright file="IGameObject.cs" company="MY Soft Games LLC">
//      Copyright (c) MY Soft Games LLC. All rights reserved.
// </copyright>

namespace MY3DEngine.Interfaces
{
    using System;
    using System.ComponentModel;
    using SharpDX;

    /// <summary>
    /// Interface for the basic properties and methods of a game object (cube, triangle, sphere, mesh, etc)
    /// </summary>
    public interface IGameObject : IDisposable
    {
        /// <summary>
        /// Event to capture when a property has changed (ex. IsAlive = false)
        /// </summary>
        event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the unique identifier of the object
        /// </summary>
        Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the number of indexes to draw
        /// </summary>
        int IndexCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether designate whether the object is a primitive type of Cube
        /// </summary>
        bool IsCube { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether designate whether the object is a primitive type
        /// </summary>
        bool IsPrimitive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether determine if the current object is selected
        /// </summary>
        bool IsSelected { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether designate whether the object is a primitive type of Triangle
        /// </summary>
        bool IsTriangle { get; set; }

        /// <summary>
        /// Gets or sets the name of the object
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets where the object is located in the world
        /// </summary>
        Vector3 Position { get; set; }

        /// <summary>
        /// Change the color of the object
        /// </summary>
        void ApplyColor();

        /// <summary>
        /// Run the actual GPU draw call
        /// </summary>
        void Draw();

        /// <summary>
        /// Load the content of the object
        /// </summary>
        void LoadContent(bool isNewObject = true);

        /// <summary>
        /// Render the object(s) content(s) to the screen
        /// </summary>
        void Render();
    }
}