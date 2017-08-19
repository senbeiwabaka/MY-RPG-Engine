using MY3DEngine.BaseObjects;
using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D11;
using SharpDX.DXGI;

namespace MY3DEngine.Primitives
{
    public class Triangle : GameObject
    {
        public Triangle() : base()
        {
            this.Name = "Triangle";
        }

        public override void LoadContent()
        {
            var path = System.IO.Path.GetFullPath("Shaders");

            // Compile Vertex shaders
            using (var vertexShaderByteCode = ShaderBytecode.CompileFromFile(string.Format("{0}\\Triangle.fx", path), "VS", "vs_4_0", ShaderFlags.EnableStrictness | ShaderFlags.Debug, EffectFlags.None))
            {
                this.VertextShader = new VertexShader(Engine.GameEngine.GraphicsManager.GetDevice, vertexShaderByteCode);

                this.inputLayout = new InputLayout(
                    Engine.GameEngine.GraphicsManager.GetDevice,
                    ShaderSignature.GetInputSignature(vertexShaderByteCode),
                    new[]
                    {
                        new InputElement("POSITION", 0, Format.R32G32B32A32_Float, 0, 0),
                        new InputElement("COLOR", 0, Format.R32G32B32A32_Float, 16, 0)
                    });
            }

            // Compile Pixel shaders
            using (var pixelShaderByteCode = ShaderBytecode.CompileFromFile(string.Format("{0}\\Triangle.fx", path), "PS", "ps_4_0", ShaderFlags.EnableStrictness | ShaderFlags.Debug, EffectFlags.None))
            {
                this.PixelShader = new PixelShader(Engine.GameEngine.GraphicsManager.GetDevice, pixelShaderByteCode);
            }

            // Instantiate Vertex buffer from vertex data
            this.buffer = Buffer.Create(
                Engine.GameEngine.GraphicsManager.GetDevice,
                BindFlags.VertexBuffer,
                new[]
                {
                        new Vector4(0.0f, 0.5f, 0.5f, 1.0f), new Vector4(1.0f, 1.0f, 1.0f, 1.0f),
                        new Vector4(0.5f, -0.5f, 0.5f, 1.0f), new Vector4(1.0f, 1.0f, 1.0f, 1.0f),
                        new Vector4(-0.5f, -0.5f, 0.5f, 1.0f), new Vector4(1.0f, 1.0f, 1.0f, 1.0f)
                });
        }

        public override void Render()
        {
            Engine.GameEngine.GraphicsManager.GetDeviceContext.InputAssembler.InputLayout = this.inputLayout;
            Engine.GameEngine.GraphicsManager.GetDeviceContext.InputAssembler.PrimitiveTopology = SharpDX.Direct3D.PrimitiveTopology.TriangleList;
            Engine.GameEngine.GraphicsManager.GetDeviceContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(this.buffer, 32, 0));
            
            Engine.GameEngine.GraphicsManager.GetDeviceContext.VertexShader.Set(this.VertextShader);
            Engine.GameEngine.GraphicsManager.GetDeviceContext.PixelShader.Set(this.PixelShader);
            Engine.GameEngine.GraphicsManager.GetDeviceContext.Draw(3, 0);
        }
    }
}