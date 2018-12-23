// <copyright file="ICamera.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MY3DEngine.Interfaces
{
    using SharpDX;

    /// <summary>
    /// Interface to build camera to see the world
    /// </summary>
    public interface ICamera
    {
        /// <summary>
        /// Gets the position of the camera
        /// </summary>
        Vector3 Position { get; }

        /// <summary>
        /// Gets the rotation of the camera
        /// </summary>
        Vector3 Rotation { get; }

        /// <summary>
        /// Gets or sets the view of the camera in the world
        /// </summary>
        Matrix ViewMatrix { get; set; }

        /// <summary>
        /// Initialize the Camera variables
        /// </summary>
        void Initialize();

        /// <summary>
        /// Initialize the Camera variables
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        void Initialize(int width, int height);

        void OnResize(float new_width, float new_height);

        void Render();

        /// <summary>
        /// Reset the camera to look at the inital position of the world (0, 0, 0)
        /// </summary>
        void ResetCamera();

        /// <summary>
        /// Set the postion of the camera
        /// </summary>
        /// <param name="newPosition">Use a Directx Vector3</param>
        void SetPosition(Vector3 newPosition);

        /// <summary>
        /// Set the postion of the camera
        /// </summary>
        /// <param name="x">The x-axis</param>
        /// <param name="y">the y-axis</param>
        /// <param name="z">the z-axis</param>
        void SetPosition(float x, float y, float z);

        /// <summary>
        /// Set the rotation of the camera
        /// </summary>
        /// <param name="rotation"></param>
        void SetRotation(Vector3 rotation);

        /// <summary>
        /// Set the rotation of the camera
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        void SetRotation(float x, float y, float z);
    }
}
