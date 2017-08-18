using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D11;
using SharpDX.DXGI;

namespace MY3DEngine.Primitives
{
    public class Triangle : GameObject
    {
        public Triangle():base()
        {
            this.Name = "Triangle";
        }

        public override void LoadContent()
        {
            // Compile Vertex shaders
            using (var vertexShaderByteCode = ShaderBytecode.CompileFromFile("SolidGreenColor.fx", "VS", "vs_4_0", ShaderFlags.None, EffectFlags.None))
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
            using (var pixelShaderByteCode = ShaderBytecode.CompileFromFile("SolidGreenColor.fx", "PS", "ps_4_0", ShaderFlags.None, EffectFlags.None))
            {
                this.PixelShader = new PixelShader(Engine.GameEngine.GraphicsManager.GetDevice, pixelShaderByteCode);

                // Instantiate Vertex buiffer from vertex data
                var vertices = Buffer.Create(
                    Engine.GameEngine.GraphicsManager.GetDevice,
                    BindFlags.VertexBuffer,
                    new[]
                    {
                        new Vector4(0.0f, 0.5f, 0.5f, 1.0f), new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                        new Vector4(0.5f, -0.5f, 0.5f, 1.0f), new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                        new Vector4(-0.5f, -0.5f, 0.5f, 1.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f)
                    });
            }
        }
    }
}