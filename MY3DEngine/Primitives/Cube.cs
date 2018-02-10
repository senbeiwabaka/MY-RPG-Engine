using MY3DEngine.BaseObjects;
using MY3DEngine.GraphicObjects;
using SharpDX;
using SharpDX.Direct3D11;

namespace MY3DEngine.Primitives
{
    /// <summary>
    /// Base object that can have color applied
    /// </summary>
    public sealed class Cube : BaseObject
    {
        /// <summary>
        /// Base constructor for the cube
        /// </summary>
        public Cube() : base(name: "Cube")
        {
            this.IsPrimitive = true;
            this.IsTriangle = false;
            this.IsCube = true;
        }

        /// <inheritdoc/>
        public override void Draw()
        {
            Engine.GameEngine.GraphicsManager.GetDeviceContext.DrawIndexed(this.IndexCount, 0, 0);
        }

        /// <inheritdoc/>
        public override void LoadContent(bool isNewObject = true)
        {
            base.LoadContent(isNewObject);

            this.Vertexies = new ColorVertex[4];

            if (isNewObject)
            {
                this.Vertexies = new ColorVertex[]
                {
                        new ColorVertex(new Vector3(-1f, -1f, 0f), new Vector4(1.0f, 1.0f, 1.0f, 1.0f)), // Bottom Left
                        new ColorVertex(new Vector3(-1f, 1f, 0f), new Vector4(1.0f, 1.0f, 1.0f, 1.0f)), // Top Left
                        new ColorVertex(new Vector3(1f, 1f, 0f), new Vector4(1.0f, 1.0f, 1.0f, 1.0f)), // Top Right
                        new ColorVertex(new Vector3(1f, -1f, 0f), new Vector4(1.0f, 1.0f, 1.0f, 1.0f)) // Bottom Right
                };

                this.Indices = new int[]
                {
                    0, 1, 2,
                    0, 2, 3
                };
            }

            // Instantiate Index Buffer from index data
            this.IndexBuffer = Buffer.Create(Engine.GameEngine.GraphicsManager.GetDevice, BindFlags.IndexBuffer, this.Indices);
            // Instantiate Vertex buffer from vertex data
            this.VertexBuffer = Buffer.Create(Engine.GameEngine.GraphicsManager.GetDevice, BindFlags.VertexBuffer, this.Vertexies);

            this.IndexCount = this.Indices.Length;
        }

        /// <inheritdoc/>
        public override void Render()
        {
            // Set the vertex buffer to active in the input assembler so it can be rendered.
            Engine.GameEngine.GraphicsManager.GetDeviceContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(this.VertexBuffer, ColorVertex.Size, 0));

            // Set the index buffer to active in the input assembler so it can be rendered.
            Engine.GameEngine.GraphicsManager.GetDeviceContext.InputAssembler.SetIndexBuffer(this.IndexBuffer, SharpDX.DXGI.Format.R32_UInt, 0);

            // Set the type of the primitive that should be rendered from this vertex buffer, in this case triangles.
            Engine.GameEngine.GraphicsManager.GetDeviceContext.InputAssembler.PrimitiveTopology = SharpDX.Direct3D.PrimitiveTopology.TriangleList;
        }
    }
}