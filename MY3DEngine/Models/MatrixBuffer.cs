namespace MY3DEngine.Models
{
    using System.Runtime.InteropServices;
    using SharpDX;

    [StructLayout(LayoutKind.Sequential)]
    public struct MatrixBuffer
    {
        public MatrixBuffer(Matrix world, Matrix view, Matrix projection)
        {
            this.World = world;
            this.View = view;
            this.Projection = projection;
        }

        public Matrix World { get; }
        public Matrix View;
        public Matrix Projection;
    }
}
