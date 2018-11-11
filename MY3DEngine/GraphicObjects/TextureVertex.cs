namespace MY3DEngine.GraphicObjects
{
    using System.Runtime.InteropServices;
    using SharpDX;

    [StructLayout(LayoutKind.Sequential)]
    public struct TextureVertex
    {
        /// <summary>
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
        /// Position of the object in the world
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        /// Texture (image) applied to the object
        /// </summary>
        public Vector2 Texture { get; set; }

        /// <summary>
        /// How big the object is for transfering data and building buffers
        /// </summary>
        public static int Size => SharpDX.Utilities.SizeOf<TextureVertex>();
    }
}