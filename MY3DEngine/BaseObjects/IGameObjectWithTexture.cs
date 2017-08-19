using SharpDX.Direct3D11;

namespace MY3DEngine.BaseObjects
{
    public interface IGameObjectWithTexture
    {
        ShaderResourceView ColorMap { get; set; }
        SamplerState ColorMapSampler { get; set; }
    }
}