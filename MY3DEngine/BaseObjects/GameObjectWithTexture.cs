using Newtonsoft.Json;
using SharpDX.Direct3D11;

namespace MY3DEngine.BaseObjects
{
    public class GameObjectWithTexture : GameObject, IGameObjectWithTexture
    {
        [JsonIgnore]
        public ShaderResourceView ColorMap { get; set; }

        [JsonIgnore]
        public SamplerState ColorMapSampler { get; set; }
    }
}