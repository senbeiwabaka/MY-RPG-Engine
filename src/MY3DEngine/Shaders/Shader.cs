// <copyright file="Shader.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using MY3DEngine.BaseObjects;
using MY3DEngine.Interfaces;
using MY3DEngine.Models;
using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D11;

namespace MY3DEngine.Shaders
{
    /// <inherietdoc/>
    internal class Shader : IShader
    {
        private InputLayout inputLayout;
        private PixelShader pixelShader;
        private VertexShader vertextShader;

        internal Shader()
        {
        }

        ~Shader()
        {
            Dispose(false);
        }

        /// <inherietdoc/>
        public SharpDX.Direct3D11.Buffer ConstantMatrixBuffer { get; set; }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <inherietdoc/>
        public void Initialize()
        {
            var path = Engine.GameEngine.SettingsManager.Settings.ShaderPath;

            // Compile Vertex shaders
            using (var vertexShaderByteCode = ShaderBytecode.CompileFromFile($"{path}\\Color.vs", "ColorVertexShader", "vs_4_0", ShaderFlags.None, EffectFlags.None))
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
                                InstanceDataStepRate = 0
                            },
                            new InputElement
                            {
                                SemanticName = "COLOR",
                                SemanticIndex = 0,
                                Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                                Slot = 0,
                                AlignedByteOffset = InputElement.AppendAligned,
                                Classification = InputClassification.PerVertexData,
                                InstanceDataStepRate = 0
                            }
                    });
            }

            // Compile Pixel shaders
            using (var pixelShaderByteCode = ShaderBytecode.CompileFromFile(string.Format("{0}\\Color.ps", path), "ColorPixelShader", "ps_4_0", ShaderFlags.None, EffectFlags.None))
            {
                this.pixelShader = new PixelShader(Engine.GameEngine.GraphicsManager.GetDevice, pixelShaderByteCode);
            }

            var matrixBufDesc = new BufferDescription()
            {
                Usage = ResourceUsage.Dynamic,
                SizeInBytes = SharpDX.Utilities.SizeOf<MatrixBuffer>(),
                BindFlags = BindFlags.ConstantBuffer,
                CpuAccessFlags = CpuAccessFlags.Write,
                OptionFlags = ResourceOptionFlags.None,
                StructureByteStride = 0
            };

            this.ConstantMatrixBuffer = new SharpDX.Direct3D11.Buffer(Engine.GameEngine.GraphicsManager.GetDevice, matrixBufDesc);

            // Set the vertex input layout.
            Engine.GameEngine.GraphicsManager.GetDeviceContext.InputAssembler.InputLayout = this.inputLayout;

            // Set the vertex and pixel shaders that will be used to render this triangle.
            Engine.GameEngine.GraphicsManager.GetDeviceContext.VertexShader.Set(this.vertextShader);
            Engine.GameEngine.GraphicsManager.GetDeviceContext.PixelShader.Set(this.pixelShader);
        }

        /// <inherietdoc/>
        public bool Render(IEnumerable<BaseObject> gameObjects, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix)
        {
            if (!SetShaderParameters(worldMatrix, viewMatrix, projectionMatrix))
            {
                return false;
            }

            Vector3 position;

            lock (gameObjects)
            {
                foreach (var gameObject in gameObjects)
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

        protected virtual void Dispose(bool dispose)
        {
            if (dispose)
            {
                this.ConstantMatrixBuffer?.Dispose();
                this.ConstantMatrixBuffer = null;
                this.inputLayout?.Dispose();
                this.inputLayout = null;
                this.pixelShader?.Dispose();
                this.pixelShader = null;
                this.vertextShader?.Dispose();
                this.vertextShader = null;
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
