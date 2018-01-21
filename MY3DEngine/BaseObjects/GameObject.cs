using Newtonsoft.Json;
using SharpDX.Direct3D11;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MY3DEngine.BaseObjects
{
    public abstract class GameObject : IGameObject, IDisposable, INotifyPropertyChanged
    {
        private string name;

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

        public string FileName { get; set; }

        public string FilePath { get; set; }

        public Guid Id { get; set; } = Guid.NewGuid();

        public bool IsCube { get; set; } = false;

        public bool IsPrimitive { get; set; } = false;

        public bool IsSelected { get; set; } = false;

        public bool IsTriangle { get; set; } = false;

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

        public int IndexCount { get; set; } = 3;

        //[XmlIgnore]
        //public MeshClass MeshObject { get; protected set; }

        [JsonIgnore]
        protected SharpDX.Direct3D11.Buffer VertexBuffer { get; set; }

        [JsonIgnore]
        protected SharpDX.Direct3D11.Buffer IndexBuffer { get; set; }

        //[JsonIgnore]
        public Vertex[] Vertexies { get; set; }

        /// <summary>
        ///
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <inheritdoc/>
        public virtual void LoadContent(bool isNewObject = true)
        {
        }

        /// <inheritdoc/>
        public virtual void Render() { }

        public virtual void Draw() { }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{Name}";

        /// <summary>
        /// Disposes of the object's mesh
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.VertexBuffer?.Dispose();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="propertyName"></param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <inheritdoc/>
        public void ApplyColor()
        {
            if (this.IsPrimitive)
            {
                this.VertexBuffer = SharpDX.Direct3D11.Buffer.Create(Engine.GameEngine.GraphicsManager.GetDevice, BindFlags.VertexBuffer, this.Vertexies);
            }
        }
    }
}