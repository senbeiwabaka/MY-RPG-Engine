using SharpDX.Direct3D11;

namespace MY3DEngine.BaseObjects
{
    public class GameObjectWithTexture : GameObject, IGameObjectWithTexture
    {
        public ShaderResourceView ColorMap { get; set; }

        public SamplerState ColorMapSampler { get; set; }
    }
}