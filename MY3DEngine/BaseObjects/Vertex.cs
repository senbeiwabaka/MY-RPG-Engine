using SharpDX;

namespace MY3DEngine.BaseObjects
{
    public struct Vertex
    {
        public Vertex(Vector4 position = default(Vector4), Vector4 color = default(Vector4))
        {
            this.Postion = position;
            this.Color = color;
        }

        public Vector4 Postion { get; set; }

        public Vector4 Color { get; set; }
    }
}