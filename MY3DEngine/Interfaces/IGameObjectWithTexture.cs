namespace MY3DEngine.Interfaces
{
    using MY3DEngine.GraphicObjects;
    using SharpDX.Direct3D11;

    public interface IGameObjectWithTexture
    {
        /// <summary>
        /// Gets or sets file name of the texture to use
        /// </summary>
        string FileName { get; set; }

        /// <summary>
        /// Gets or sets file path of the texture to use
        /// </summary>
        string FilePath { get; set; }

        /// <summary>
        /// Gets or sets the building blocks of the object
        /// </summary>
        TextureVertex[] TextureVerticies { get; set; }
    }
}