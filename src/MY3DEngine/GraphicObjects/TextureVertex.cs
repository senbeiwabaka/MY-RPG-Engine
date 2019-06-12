// <copyright file="TextureVertex.cs" company="MY Soft Games LLC">
//      Copyright (c) MY Soft Games LLC. All rights reserved.
// </copyright>

namespace MY3DEngine.GraphicObjects
{
    using System.Runtime.InteropServices;
    using SharpDX;

    [StructLayout(LayoutKind.Sequential)]
    public struct TextureVertex
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextureVertex"/> struct.
        /// Constructor
        /// </summary>
        /// <param name="position"></param>
        /// <param name="texture"></param>
        public TextureVertex(Vector3 position = default(Vector3), Vector2 texture = default(Vector2))
        {
            this.Position = position;
            this.Texture = texture;
        }

        /// <summary>
        /// Gets or sets position of the object in the world
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        /// Gets or sets texture (image) applied to the object
        /// </summary>
        public Vector2 Texture { get; set; }

        /// <summary>
        /// Gets how big the object is for transfering data and building buffers
        /// </summary>
        public static int Size => Utilities.SizeOf<TextureVertex>();
    }
}
