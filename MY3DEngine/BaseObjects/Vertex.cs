using SharpDX;
using SharpDX.Mathematics.Interop;
using System.Runtime.InteropServices;

namespace MY3DEngine.BaseObjects
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vertex
    {
        public Vertex(Vector3 position = default(Vector3), Vector4 color = default(Vector4))
        {
            this.Postion = position;
            this.Color = color;
        }

        public Vector3 Postion { get; set; }

        public Vector4 Color { get; set; }

        public static int Size => Utilities.SizeOf<Vertex>();
    }
}