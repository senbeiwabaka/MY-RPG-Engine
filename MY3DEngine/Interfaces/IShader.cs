namespace MY3DEngine.Interfaces
{
    using System;
    using System.Collections.Generic;
    using MY3DEngine.BaseObjects;
    using SharpDX;

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
        void Initialize();

        /// <summary>
        /// Render all of the objects to the screen
        /// </summary>
        /// <param name="gameObjects"></param>
        /// <param name="worldMatrix"></param>
        /// <param name="viewMatrix"></param>
        /// <param name="projectionMatrix"></param>
        bool Render(IEnumerable<BaseObject> gameObjects, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix);
    }
}
