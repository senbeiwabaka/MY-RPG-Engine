using SharpDX.Direct3D11;
using System;

namespace MY3DEngine.BaseObjects
{
    public interface IGameObject
    {
        string FileName { get; set; }

        string FilePath { get; set; }

        Guid Id { get; set; }

        bool IsCube { get; set; }

        bool IsPrimitive { get; set; }

        bool IsSelected { get; set; }

        bool IsTriangle { get; set; }

        string Name { get; set; }

        PixelShader PixelShader { get; set; }

        VertexShader VertextShader { get; set; }
    }
}