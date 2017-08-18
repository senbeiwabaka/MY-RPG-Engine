﻿using SharpDX.Direct3D11;
using System;

namespace MY3DEngine
{
    public interface IObject
    {
        string FileName { get; set; }

        string FilePath { get; set; }

        string Name { get; set; }

        Guid Id { get; set; }

        PixelShader PixelShader { get; set; }

        VertexShader VertextShader { get; set; }

        bool IsSelected { get; set; }
    }
}