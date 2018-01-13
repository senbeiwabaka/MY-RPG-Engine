using MY3DEngine.BaseObjects;
using Newtonsoft.Json;
using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;

namespace MY3DEngine.Primitives
{
    public class Cube : GameObject
    {
        public Cube() : base("Cube")
        {
            this.IsPrimitive = true;
            this.IsTriangle = false;
            this.IsCube = true;
        }

        [JsonIgnore]
        public Buffer IndexBuffer { get; set; }

        public int[] Indices { get; set; } = new int[6]
            {
                0, 1, 2,
                0, 2, 3
            };

        /// <inheritdoc/>
        public override void LoadContent(bool isNewObject = true)
        {
            base.LoadContent(isNewObject);

            if (isNewObject)
            {
                this.Vertex = new Vertex[]
                {
                        new Vertex(new Vector4(-0.5f, -0.5f, 0.5f, 1.0f), new Vector4(1.0f, 1.0f, 1.0f, 1.0f)), // Top Left
                        new Vertex(new Vector4(-0.5f, 0.5f, 0.5f, 1.0f), new Vector4(1.0f, 1.0f, 1.0f, 1.0f)), // Top Right
                        new Vertex(new Vector4(0.5f, 0.5f, 0.5f, 1.0f), new Vector4(1.0f, 1.0f, 1.0f, 1.0f)), // Bottom Left
                        new Vertex(new Vector4(0.5f, -0.5f, 0.5f, 1.0f), new Vector4(1.0f, 1.0f, 1.0f, 1.0f)) // Bottom Right
                };
            }

            // Instantiate Index Buffer from index data
            this.IndexBuffer = Buffer.Create(
                Engine.GameEngine.GraphicsManager.GetDevice,
                BindFlags.IndexBuffer,
                this.Indices);

            // Instantiate Vertex buffer from vertex data
            this.Buffer = Buffer.Create(
                Engine.GameEngine.GraphicsManager.GetDevice,
                BindFlags.VertexBuffer,
                this.Vertex);

            Engine.GameEngine.GraphicsManager.GetDeviceContext.InputAssembler.InputLayout = this.InputLayout;
            Engine.GameEngine.GraphicsManager.GetDeviceContext.InputAssembler.PrimitiveTopology = SharpDX.Direct3D.PrimitiveTopology.TriangleList;
            Engine.GameEngine.GraphicsManager.GetDeviceContext.InputAssembler.SetIndexBuffer(this.IndexBuffer, Format.R32_UInt, 0);

            Engine.GameEngine.GraphicsManager.GetDeviceContext.VertexShader.Set(this.VertextShader);
            Engine.GameEngine.GraphicsManager.GetDeviceContext.PixelShader.Set(this.PixelShader);

            Engine.GameEngine.GraphicsManager.GetDeviceContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(this.Buffer, BaseObjects.Vertex.Size, 0));
        }

        /// <inheritdoc/>
        public override void Render()
        {
            Engine.GameEngine.GraphicsManager.GetDeviceContext.DrawIndexed(6, 0, 0);
        }
    }
}