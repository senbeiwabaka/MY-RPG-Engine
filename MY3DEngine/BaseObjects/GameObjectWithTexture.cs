using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct3D11;

namespace MY3DEngine.BaseObjects
{
    public class GameObjectWithTexture : GameObject, IGameObjectWithTexture
    {
        public ShaderResourceView ColorMap { get; set; }

        public SamplerState ColorMapSampler { get; set; }
    }
}