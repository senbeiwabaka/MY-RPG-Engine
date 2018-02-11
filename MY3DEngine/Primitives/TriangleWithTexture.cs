using MY3DEngine.BaseObjects;
using MY3DEngine.GraphicObjects;
using MY3DEngine.Managers;
using MY3DEngine.Utilities;
using SharpDX;
using SharpDX.Direct3D11;
using System.IO;
using wic = SharpDX.WIC;

namespace MY3DEngine.Primitives
{
    public class TriangleWithTexture : GameObjectWithTexture
    {
        public TriangleWithTexture()
            :base(name: "Triangle with Texture")
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
            base.LoadContent(isNewObject);
            
            var path = Engine.GameEngine.SettingsManager.Settings.AssetsPath;
            path = string.Format("{0}\\seafloor.bmp", path);

            TextureManager.Initialize(Engine.GameEngine.GraphicsManager.GetDevice, path, this.Texture);
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