namespace MY3DEngine.Primitives
{
    using MY3DEngine.BaseObjects;
    using MY3DEngine.GraphicObjects;
    using MY3DEngine.Managers;
    using SharpDX;
    using SharpDX.Direct3D11;

    /// <summary>
    /// A basic triangle with a texture applied
    /// </summary>
    public sealed class TriangleWithTexture : GameObjectWithTexture
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TriangleWithTexture"/> class.
        /// A basic triangle with a texture applied
        /// </summary>
        public TriangleWithTexture()
            : base(name: "Triangle with Texture")
        {
        }

        /// <inheritdoc/>
        public override void Draw()
        {
            Engine.GameEngine.GraphicsManager.GetDeviceContext.DrawIndexed(this.IndexCount, 0, 0);
        }

        /// <inheritdoc/>
        public override void LoadContent(bool isNewObject = true)
        {
            // Set number of vertices in the index array.
            IndexCount = 3;

            // Create the vertex array and load it with data.
            this.TextureVerticies = new[]
            {
                    // Bottom left.
                    new TextureVertex
                    {
                        Position = new Vector3(-1, -1, 0),
                        Texture = new Vector2(0, 1)
                    },

                    // Top middle.
                    new TextureVertex
                    {
                        Position = new Vector3(0, 1, 0),
                        Texture = new Vector2(.5f, 0)
                    },

                    // Bottom right.
                    new TextureVertex
                    {
                        Position = new Vector3(1, -1, 0),
                        Texture = new Vector2(1, 1)
                    }
                };

            // Create Indicies to load into the IndexBuffer.
            this.Indices = new int[] {
                    0, // Bottom left.
                    1, // Top middle.
                    2 // Bottom right.
            };

            // Instantiate Vertex buffer from vertex data
            this.VertexBuffer = Buffer.Create(Engine.GameEngine.GraphicsManager.GetDevice, BindFlags.VertexBuffer, this.TextureVerticies);

            // Instantiate Index Buffer from index data
            this.IndexBuffer = Buffer.Create(Engine.GameEngine.GraphicsManager.GetDevice, BindFlags.IndexBuffer, this.Indices);

            var path = Engine.GameEngine.SettingsManager.Settings.AssetsPath;
            path = $"{path}\\seafloor.bmp";

            TextureManager.Initialize(Engine.GameEngine.GraphicsManager.GetDevice, path, ref this.Texture);
        }

        /// <inheritdoc/>
        public override void Render()
        {
            // Set the vertex buffer to active in the input assembler so it can be rendered.
            Engine.GameEngine.GraphicsManager.GetDeviceContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(this.VertexBuffer, TextureVertex.Size, 0));

            // Set the index buffer to active in the input assembler so it can be rendered.
            Engine.GameEngine.GraphicsManager.GetDeviceContext.InputAssembler.SetIndexBuffer(this.IndexBuffer, SharpDX.DXGI.Format.R32_UInt, 0);

            // Set the type of the primitive that should be rendered from this vertex buffer, in this case triangles.
            Engine.GameEngine.GraphicsManager.GetDeviceContext.InputAssembler.PrimitiveTopology = SharpDX.Direct3D.PrimitiveTopology.TriangleList;

            // Set shader resource in the pixel shader.
            Engine.GameEngine.GraphicsManager.GetDeviceContext.PixelShader.SetShaderResource(0, this.Texture);
        }
    }
}