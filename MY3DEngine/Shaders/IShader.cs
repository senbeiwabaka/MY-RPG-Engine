using MY3DEngine.BaseObjects;
using SharpDX;
using System;
using System.Collections.Generic;

namespace MY3DEngine.Shaders
{
    public interface IShader : IDisposable
    {
        SharpDX.Direct3D11.Buffer ConstantMatrixBuffer { get; set; }

        bool Initialize();

        bool Render(IEnumerable<GameObject> gameObjects, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix);
    }
}