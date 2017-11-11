using SharpDX.Direct3D11;
using System;

namespace MY3DEngine.BaseObjects
{
    public interface IGameObject
    {
        string FileName { get; set; }

        string FilePath { get; set; }

        Guid Id { get; set; }

        /// <summary>
        /// Designate whether the object is a primitive type of Cube
        /// </summary>
        bool IsCube { get; set; }

        /// <summary>
        /// Designate whether the object is a primitive type
        /// </summary>
        bool IsPrimitive { get; set; }

        bool IsSelected { get; set; }

        bool IsTriangle { get; set; }

        string Name { get; set; }

        PixelShader PixelShader { get; set; }

        VertexShader VertextShader { get; set; }

        /// <summary>
        /// Change the color of the object
        /// </summary>
        void ApplyColor();

        /// <summary>
        /// Load the content of the object
        /// </summary>
        void LoadContent(bool isNewObject = true);

        /// <summary>
        /// Render the object(s) content(s) to the screen
        /// </summary>
        void Render();
    }
}