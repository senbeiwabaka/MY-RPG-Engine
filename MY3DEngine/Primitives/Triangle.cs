using MY3DEngine.BaseObjects;
using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D11;
using SharpDX.DXGI;

namespace MY3DEngine.Primitives
{
    /// <summary>
    /// Basic game triangle object
    /// </summary>
    public class Triangle : GameObject
    {
        /// <summary>
        /// Basic game triangle object
        /// </summary>
        public Triangle() : base()
        {
            this.Name = "Triangle";
            this.IsPrimitive = true;
            this.IsTriangle = true;
            this.IsCube = false;
        }
        
        /// <inheritdoc/>
        public override void LoadContent(bool isNewObject = true)
        {
            base.LoadContent(isNewObject);

            if (isNewObject)
            {
                this.Vertex = new Vertex[]
                    {
                        new Vertex(new Vector4(0.0f, 0.5f, 0.5f, 1.0f), new Vector4(1.0f, 1.0f, 1.0f, 1.0f)),
                        new Vertex(new Vector4(0.5f, -0.5f, 0.5f, 1.0f), new Vector4(1.0f, 1.0f, 1.0f, 1.0f)),
                        new Vertex(new Vector4(-0.5f, -0.5f, 0.5f, 1.0f), new Vector4(1.0f, 1.0f, 1.0f, 1.0f))
                    };
            }

            // Instantiate Vertex buffer from vertex data
            this.Buffer = Buffer.Create(
                Engine.GameEngine.GraphicsManager.GetDevice,
                BindFlags.VertexBuffer,
                this.Vertex);

            Engine.GameEngine.GraphicsManager.GetDeviceContext.InputAssembler.InputLayout = this.InputLayout;
            Engine.GameEngine.GraphicsManager.GetDeviceContext.InputAssembler.PrimitiveTopology = SharpDX.Direct3D.PrimitiveTopology.TriangleList;
            
            Engine.GameEngine.GraphicsManager.GetDeviceContext.VertexShader.Set(this.VertextShader);
            Engine.GameEngine.GraphicsManager.GetDeviceContext.PixelShader.Set(this.PixelShader);
        }

        /// <inheritdoc/>
        public override void Render()
        {
            Engine.GameEngine.GraphicsManager.GetDeviceContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(this.Buffer, BaseObjects.Vertex.Size, 0));

            Engine.GameEngine.GraphicsManager.GetDeviceContext.Draw(3, 0);
        }
    }
}