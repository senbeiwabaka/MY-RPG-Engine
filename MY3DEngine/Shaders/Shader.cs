using MY3DEngine.BaseObjects;
using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;

namespace MY3DEngine.Shaders
{
    public sealed class Shader : IShader
    {
        private InputLayout inputLayout;
        private PixelShader pixelShader;
        private VertexShader vertextShader;

        public SharpDX.Direct3D11.Buffer ConstantMatrixBuffer { get; set; }

        public void Dispose()
        {
            this.Dispose(true);

            GC.SuppressFinalize(true);
        }

        public bool Initialize()
        {
            try
            {
                var path = System.IO.Path.GetFullPath("Shaders");

                // Compile Vertex shaders
                using (var vertexShaderByteCode = ShaderBytecode.CompileFromFile(string.Format("{0}\\Color.vs", path), "ColorVertexShader", "vs_4_0", ShaderFlags.None, EffectFlags.None))
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
                    SizeInBytes = Utilities.SizeOf<MatrixBuffer>(), // was Matrix
                    BindFlags = BindFlags.ConstantBuffer,
                    CpuAccessFlags = CpuAccessFlags.Write,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0
                };

                this.ConstantMatrixBuffer = new SharpDX.Direct3D11.Buffer(Engine.GameEngine.GraphicsManager.GetDevice, matrixBufDesc);
            }
            catch (Exception e)
            {
                Engine.GameEngine.AddException(e);

                return false;
            }

            return true;
        }

        public bool Render(IEnumerable<GameObject> gameObjects, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix)
        {
            if (!SetShaderParameters(worldMatrix, viewMatrix, projectionMatrix))
            {
                return false;
            }

            lock (gameObjects)
            {
                foreach (var gameObject in gameObjects)
                {
                    gameObject.Render();

                    // Set the vertex input layout.
                    Engine.GameEngine.GraphicsManager.GetDeviceContext.InputAssembler.InputLayout = this.inputLayout;

                    // Set the vertex and pixel shaders that will be used to render this triangle.
                    Engine.GameEngine.GraphicsManager.GetDeviceContext.VertexShader.Set(this.vertextShader);
                    Engine.GameEngine.GraphicsManager.GetDeviceContext.PixelShader.Set(this.pixelShader);

                    gameObject.Draw();
                }
            }

            return true;
        }

        private void Dispose(bool dispose)
        {
            if (dispose)
            {
                this.ConstantMatrixBuffer?.Dispose();
                this.ConstantMatrixBuffer = null;
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

                var matrixBuffer = new MatrixBuffer
                {
                    world = worldMatrix,
                    view = viewMatrix,
                    projection = projectionMatrix
                };

                mappedResource.Write(matrixBuffer);

                Engine.GameEngine.GraphicsManager.GetDeviceContext.UnmapSubresource(this.ConstantMatrixBuffer, 0);

                var bufferSlotNuber = 0;

                Engine.GameEngine.GraphicsManager.GetDeviceContext.VertexShader.SetConstantBuffer(bufferSlotNuber, this.ConstantMatrixBuffer);
            }
            catch (Exception e)
            {
                Engine.GameEngine.AddException(e);

                return false;
            }

            return true;
        }
    }
}