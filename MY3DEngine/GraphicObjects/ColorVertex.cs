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
        /// Position of the object in the world
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        /// Color of the object in the world
        /// </summary>
        public Vector4 Color { get; set; }

        /// <summary>
        /// How big the object is for transfering data and building buffers
        /// </summary>
        public static int Size => SharpDX.Utilities.SizeOf<ColorVertex>();
    }
}