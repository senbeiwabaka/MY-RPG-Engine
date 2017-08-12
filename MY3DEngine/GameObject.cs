using SharpDX.Direct3D11;
using System;

namespace MY3DEngine
{
    [Serializable]
    public abstract class GameObject : IObject, IDisposable
    {
        public string FileName { get; set; }

        public string FilePath { get; set; }

        public Guid Id { get; set; }

        public string Name { get; set; }

        //[XmlIgnore]
        //public MeshClass MeshObject { get; protected set; }

        public PixelShader PixelShader { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public VertexShader VertextShader { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        protected virtual SharpDX.Direct3D11.Buffer buffer { get; set; }

        protected virtual InputLayout inputLayout { get; set; }

        #region Constructors

        /// <summary>
        /// blank constructor
        /// </summary>
        public GameObject()
        {
            this.Id = Guid.NewGuid();
        }

        /// <summary>
        /// Constructor for building a .x object
        /// </summary>
        /// <param name="fileName">The file name of the object</param>
        /// <param name="path">The path of the object</param>
        public GameObject(string fileName = "", string path = ""):this()
        {
            //MeshObject = new MeshClass(path, fileName);
            FileName = fileName;
            FilePath = path;
        }

        public GameObject(string type = "Cube"): this()
        {
            //if (type.ToLower().Equals("triangle"))
            //{
            //    MeshObject = new MeshClass(MeshType.Triangle);
            //}
            //else
            //{
            //    MeshObject = new MeshClass(MeshType.Cube);
            //}

            Name = type;
        }

        #endregion Constructors

        public virtual void LoadContent() { }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Renderer()
        {
            //if (MeshObject != null)
            //{
            //    MeshObject.RenderMesh();
            //}
        }

        public override string ToString() => $"{Id}.{Name}";

        /// <summary>
        /// Disposes of the object's mesh
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.buffer?.Dispose();
                this.inputLayout?.Dispose();
                this.VertextShader?.Dispose();
                this.PixelShader?.Dispose();
            }
        }
    }
}