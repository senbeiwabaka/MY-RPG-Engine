// <copyright file="ColorVertex.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MY3DEngine.GraphicObjects
{
    using System.Runtime.InteropServices;
    using SharpDX;

    /// <summary>
    /// The Color Vertex for graphics
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ColorVertex
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ColorVertex"/> struct.
        /// Constructor
        /// </summary>
        /// <param name="position"></param>
        /// <param name="color"></param>
        public ColorVertex(Vector3 position = default(Vector3), Vector4 color = default(Vector4))
        {
            this.Position = position;
            this.Color = color;
        }

        /// <summary>
        /// Gets or sets position of the object in the world
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        /// Gets or sets color of the object in the world
        /// </summary>
        public Vector4 Color { get; set; }

        /// <summary>
        /// Gets how big the object is for transfering data and building buffers
        /// </summary>
        public static int Size => SharpDX.Utilities.SizeOf<ColorVertex>();
    }
}