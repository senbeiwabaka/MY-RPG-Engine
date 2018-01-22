using MY3DEngine.BaseObjects;
using SharpDX;
using System;
using System.Collections.Generic;

namespace MY3DEngine.Shaders
{
    /// <summary>
    /// Interface for using shaders in the engine
    /// </summary>
    public interface IShader : IDisposable
    {
        /// <summary>
        /// The constant buffer that has the resources mapped to it.
        /// </summary>
        SharpDX.Direct3D11.Buffer ConstantMatrixBuffer { get; set; }

        /// <summary>
        /// Initialize the required memory and datasets
        /// </summary>
        /// <returns></returns>
        bool Initialize();

        /// <summary>
        /// Render all of the objects to the screen
        /// </summary>
        /// <param name="gameObjects"></param>
        /// <param name="worldMatrix"></param>
        /// <param name="viewMatrix"></param>
        /// <param name="projectionMatrix"></param>
        /// <returns></returns>
        bool Render(IEnumerable<GameObject> gameObjects, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix);
    }
}