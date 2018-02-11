using MY3DEngine.GraphicObjects;
using SharpDX.Direct3D11;

namespace MY3DEngine.BaseObjects
{
    public interface IGameObjectWithTexture
    {
        ShaderResourceView Texture { get; set; }

        /// <summary>
        /// File name of the texture to use
        /// </summary>
        string FileName { get; set; }

        /// <summary>
        /// File path of the texture to use
        /// </summary>
        string FilePath { get; set; }

        /// <summary>
        /// The building blocks of the object
        /// </summary>
        TextureVertex[] TextureVerticies { get; set; }
    }
}