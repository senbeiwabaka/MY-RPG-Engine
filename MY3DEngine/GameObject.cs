using SharpDX.Direct3D11;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MY3DEngine
{
    [Serializable]
    public abstract class GameObject : IObject, IDisposable, INotifyPropertyChanged
    {
        private string name;

        public string FileName { get; set; }

        public string FilePath { get; set; }

        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = value;

                this.NotifyPropertyChanged(nameof(this.Name));
            }
        }

        //[XmlIgnore]
        //public MeshClass MeshObject { get; protected set; }

        public PixelShader PixelShader { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public VertexShader VertextShader { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        protected virtual SharpDX.Direct3D11.Buffer buffer { get; set; }

        protected virtual InputLayout inputLayout { get; set; }

        public bool IsSelected { get; set; } = false;

        #region Constructors

        /// <summary>
        /// blank constructor
        /// </summary>
        public GameObject() { }

        /// <summary>
        /// Constructor for building a .x object
        /// </summary>
        /// <param name="fileName">The file name of the object</param>
        /// <param name="path">The path of the object</param>
        public GameObject(string fileName = "", string path = "")
        {
            //MeshObject = new MeshClass(path, fileName);
            FileName = fileName;
            FilePath = path;
        }

        public GameObject(string type = "Cube")
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

        public event PropertyChangedEventHandler PropertyChanged;

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

        public override string ToString() => $"{Name}";

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

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}