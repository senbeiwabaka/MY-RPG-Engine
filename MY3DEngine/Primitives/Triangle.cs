using MY3DEngine.BaseObjects;
using MY3DEngine.GraphicObjects;
using SharpDX;
using SharpDX.Direct3D11;

namespace MY3DEngine.Primitives
{
    /// <summary>
    /// Basic game triangle object
    /// </summary>
    public class Triangle : BaseObject
    {
        /// <summary>
        /// Basic game triangle object
        /// </summary>
        public Triangle() : base(name: "Triangle")
        {
            this.IsPrimitive = true;
            this.IsTriangle = true;
            this.IsCube = false;
        }

        /// <inheritdoc/>
        public override void Draw()
        {
            Engine.GameEngine.GraphicsManager.GetDeviceContext.DrawIndexed(this.IndexCount, 0, 0);
        }

        /// <inheritdoc/>
        public override void LoadContent(bool isNewObject = true)
        {
            this.Vertexies = new ColorVertex[3];

            if (isNewObject)
            {
                this.Vertexies = new ColorVertex[]
                {
                    // Bottom left.
					new ColorVertex(new Vector3(-1, -1, 0), new Vector4(1.0f, 1.0f, 1.0f, 1.0f)),
					// Top middle. TO DO 3:  Top Left.
					new ColorVertex(new Vector3(0, 1, 0), new Vector4(1.0f, 1.0f, 1.0f, 1.0f)),
					// Bottom right.
					new ColorVertex(new Vector3(1, -1, 0), new Vector4(1.0f, 1.0f, 1.0f, 1.0f))
                };

                this.Indices = new int[]
                {
                    0,
                    1,
                    2
                };
            }

            // Instantiate Vertex buffer from vertex data
            this.VertexBuffer = Buffer.Create(Engine.GameEngine.GraphicsManager.GetDevice, BindFlags.VertexBuffer, this.Vertexies);
            // Instantiate Index Buffer from index data
            this.IndexBuffer = Buffer.Create(Engine.GameEngine.GraphicsManager.GetDevice, BindFlags.IndexBuffer, this.Indices);

            this.IndexCount = this.Indices.Length;
        }

        /// <inheritdoc/>
        public override void Render()
        {
            //int size = Utilities.SizeOf<ColorVertex>();
            // Set the vertex buffer to active in the input assembler so it can be rendered.
            Engine.GameEngine.GraphicsManager.GetDeviceContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(this.VertexBuffer, ColorVertex.Size, 0));

            // Set the index buffer to active in the input assembler so it can be rendered.
            Engine.GameEngine.GraphicsManager.GetDeviceContext.InputAssembler.SetIndexBuffer(this.IndexBuffer, SharpDX.DXGI.Format.R32_UInt, 0);

            // Set the type of the primitive that should be rendered from this vertex buffer, in this case triangles.
            Engine.GameEngine.GraphicsManager.GetDeviceContext.InputAssembler.PrimitiveTopology = SharpDX.Direct3D.PrimitiveTopology.TriangleList;
        }
    }
}