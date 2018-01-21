using SharpDX;
using SharpDX.Mathematics.Interop;
using System.Runtime.InteropServices;

namespace MY3DEngine
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MatrixBuffer
    {
        public Matrix world;
        public Matrix view;
        public Matrix projection;
    }
}