// <copyright file="TextureShader.cs" company="MY Soft Games LLC">
//      Copyright (c) MY Soft Games LLC. All rights reserved.
// </copyright>

namespace MY3DEngine.Shaders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MY3DEngine.BaseObjects;
    using MY3DEngine.Interfaces;
    using MY3DEngine.Models;
    using SharpDX;
    using SharpDX.D3DCompiler;
    using SharpDX.Direct3D11;

    internal class TextureShader : IShader
    {
        private InputLayout inputLayout;
        private PixelShader pixelShader;
        private VertexShader vertextShader;
        private SamplerState samplerState;

        ~TextureShader()
        {
            Dispose(false);
        }

        /// <inherietdoc/>
        public SharpDX.Direct3D11.Buffer ConstantMatrixBuffer { get; set; }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <inheritdoc/>
        public void Initialize()
        {
            var path = Engine.GameEngine.SettingsManager.Settings.ShaderPath;

            // Compile Vertex shaders
            using (var vertexShaderByteCode = ShaderBytecode.CompileFromFile($"{path}\\texture.vs", "TextureVertexShader", "vs_5_0", ShaderFlags.EnableStrictness, EffectFlags.None))
            {
                this.vertextShader = new VertexShader(Engine.GameEngine.GraphicsManager.GetDevice, vertexShaderByteCode);

                this.inputLayout = new InputLayout(
                    Engine.GameEngine.GraphicsManager.GetDevice,
                    ShaderSignature.GetInputSignature(vertexShaderByteCode),
                    new InputElement[]
                    {
                            new InputElement
                            {
                                SemanticName = "POSITION",
                                SemanticIndex = 0,
                                Format = SharpDX.DXGI.Format.R32G32B32_Float,
                                Slot = 0,
                                AlignedByteOffset = 0,
                                Classification = InputClassification.PerVertexData,
                                InstanceDataStepRate = 0,
                            },
                            new InputElement
                            {
                                SemanticName = "TEXCOORD",
                                SemanticIndex = 0,
                                Format = SharpDX.DXGI.Format.R32G32_Float,
                                Slot = 0,
                                AlignedByteOffset = InputElement.AppendAligned,
                                Classification = InputClassification.PerVertexData,
                                InstanceDataStepRate = 0,
                            },
                    });
            }

            // Compile Pixel shaders
            using (var pixelShaderByteCode = ShaderBytecode.CompileFromFile($"{path}\\texture.ps", "TexturePixelShader", "ps_5_0", ShaderFlags.EnableStrictness, EffectFlags.None))
            {
                this.pixelShader = new PixelShader(Engine.GameEngine.GraphicsManager.GetDevice, pixelShaderByteCode);
            }

            var matrixBufDesc = new BufferDescription()
            {
                Usage = ResourceUsage.Dynamic,
                SizeInBytes = Utilities.SizeOf<MatrixBuffer>(),
                BindFlags = BindFlags.ConstantBuffer,
                CpuAccessFlags = CpuAccessFlags.Write,
                OptionFlags = ResourceOptionFlags.None,
                StructureByteStride = 0,
            };

            this.ConstantMatrixBuffer = new SharpDX.Direct3D11.Buffer(Engine.GameEngine.GraphicsManager.GetDevice, matrixBufDesc);

            var sampleStateDescription = new SamplerStateDescription
            {
                Filter = Filter.MinMagMipLinear,
                AddressU = TextureAddressMode.Wrap,
                AddressV = TextureAddressMode.Wrap,
                AddressW = TextureAddressMode.Wrap,
                MipLodBias = 0.0f,
                MaximumAnisotropy = 1,
                ComparisonFunction = Comparison.Always,
                BorderColor = new SharpDX.Mathematics.Interop.RawColor4(0.0f, 0.0f, 0.0f, 0.0f),
                MinimumLod = 0,
                MaximumLod = float.MaxValue,
            };
            this.samplerState = new SamplerState(Engine.GameEngine.GraphicsManager.GetDevice, sampleStateDescription);

            // Set the vertex input layout.
            Engine.GameEngine.GraphicsManager.GetDeviceContext.InputAssembler.InputLayout = this.inputLayout;

            // Set the vertex and pixel shaders that will be used to render this triangle.
            Engine.GameEngine.GraphicsManager.GetDeviceContext.VertexShader.Set(this.vertextShader);
            Engine.GameEngine.GraphicsManager.GetDeviceContext.PixelShader.Set(this.pixelShader);

            // Set the sampler state in the pixel shader.
            Engine.GameEngine.GraphicsManager.GetDeviceContext.PixelShader.SetSampler(0, this.samplerState);
        }

        /// <inheritdoc/>
        public bool Render(IEnumerable<BaseObject> gameObjects, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix)
        {
            if (!SetShaderParameters(worldMatrix, viewMatrix, projectionMatrix))
            {
                return false;
            }

            Vector3 position;

            lock (gameObjects)
            {
                foreach (var gameObject in gameObjects.Where(x => !x.IsPrimitive))
                {
                    position = gameObject.Position;

                    gameObject.Render();

                    // Before checking whether this model is in the view to render, adjust the position of the model to the newly rotated camera view to see if it needs to be rendered this frame or not.
                    position = Vector3.TransformCoordinate(position, worldMatrix);

                    // Move the model to the location it should be rendered at.
                    worldMatrix *= Matrix.Translation(position);

                    gameObject.Draw();
                }
            }

            return true;
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.ConstantMatrixBuffer?.Dispose();
                this.ConstantMatrixBuffer = null;
                this.inputLayout?.Dispose();
                this.inputLayout = null;
                this.pixelShader?.Dispose();
                this.pixelShader = null;
                this.vertextShader?.Dispose();
                this.vertextShader = null;
                this.samplerState?.Dispose();
                this.samplerState = null;
            }
        }

        private bool SetShaderParameters(Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix)
        {
            try
            {
                worldMatrix.Transpose();
                viewMatrix.Transpose();
                projectionMatrix.Transpose();

                Engine.GameEngine.GraphicsManager.GetDeviceContext.MapSubresource(this.ConstantMatrixBuffer, MapMode.WriteDiscard, MapFlags.None, out var mappedResource);

                var matrixBuffer = new MatrixBuffer(worldMatrix, viewMatrix, projectionMatrix);

                mappedResource.Write(matrixBuffer);

                Engine.GameEngine.GraphicsManager.GetDeviceContext.UnmapSubresource(this.ConstantMatrixBuffer, 0);

                var bufferSlotNuber = 0;

                Engine.GameEngine.GraphicsManager.GetDeviceContext.VertexShader.SetConstantBuffer(bufferSlotNuber, this.ConstantMatrixBuffer);
            }
            catch (Exception e)
            {
                Engine.GameEngine.Exception.AddException(e);

                return false;
            }

            return true;
        }
    }
}
