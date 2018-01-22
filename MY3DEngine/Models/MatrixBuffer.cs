using SharpDX;
using System.Runtime.InteropServices;

namespace MY3DEngine.Models
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MatrixBuffer
    {
        public Matrix world;
        public Matrix view;
        public Matrix projection;
    }
}