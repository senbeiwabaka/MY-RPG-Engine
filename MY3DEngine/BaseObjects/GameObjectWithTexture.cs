namespace MY3DEngine.BaseObjects
{
    using MY3DEngine.GraphicObjects;
    using MY3DEngine.Interfaces;
    using SharpDX.Direct3D11;

    /// <inheritdoc/>
    public abstract class GameObjectWithTexture : BaseObject, IGameObjectWithTexture
    {
        protected ShaderResourceView Texture;

        /// <summary>
        /// Constructor for building a .x object
        /// </summary>
        /// <param name="fileName">The file name of the object</param>
        /// <param name="path">The path of the object</param>
        /// <param name="name"></param>
        protected GameObjectWithTexture(string fileName = default(string), string path = default(string), string name = "Object")
            : base(name)
        {
            this.FileName = fileName;
            this.FilePath = path;
            this.IsPrimitive = false;
            this.IsCube = false;
            this.IsTriangle = false;
            this.IsSelected = false;
        }

        /// <inheritdoc/>
        public string FileName { get; set; }

        /// <inheritdoc/>
        public string FilePath { get; set; }

        /// <inheritdoc/>
        public TextureVertex[] TextureVerticies { get; set; }
    }
}
