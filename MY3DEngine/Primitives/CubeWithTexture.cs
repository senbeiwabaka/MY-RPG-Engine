// <copyright file="CubeWithTexture.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MY3DEngine.Primitives
{
    using System;
    using MY3DEngine.BaseObjects;
    using MY3DEngine.GraphicObjects;
    using SharpDX.Direct3D11;

    /// <summary>
    /// Holds properties and methods of a cube with texture
    /// </summary>
    public sealed class CubeWithTexture : GameObjectWithTexture
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CubeWithTexture"/> class.
        /// A basic cube with a texture applied
        /// </summary>
        public CubeWithTexture()
            : base(name: "Cube with Texture")
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
            throw new NotImplementedException();
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
