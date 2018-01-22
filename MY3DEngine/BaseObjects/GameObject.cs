using Newtonsoft.Json;
using SharpDX;
using SharpDX.Direct3D11;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MY3DEngine.BaseObjects
{
    /// <inheritdoc/>
    public abstract class GameObject : IGameObject, INotifyPropertyChanged
    {
        private string name;
        private Vector3 position;

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

        /// <inheritdoc/>
        public string FileName { get; set; }

        /// <inheritdoc/>
        public string FilePath { get; set; }

        /// <inheritdoc/>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <inheritdoc/>
        public int IndexCount { get; set; } = 3;

        /// <inheritdoc/>
        public int[] Indices { get; set; }

        /// <inheritdoc/>
        public bool IsCube { get; set; } = false;

        /// <inheritdoc/>
        public bool IsPrimitive { get; set; } = false;

        /// <inheritdoc/>
        public bool IsSelected { get; set; } = false;

        /// <inheritdoc/>
        public bool IsTriangle { get; set; } = false;

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public Vector3 Position
        {
            get
            {
                return this.position;
            }

            set
            {
                this.position = value;

                this.NotifyPropertyChanged(nameof(this.Position));
            }
        }

        /// <inheritdoc/>
        public Vertex[] Vertexies { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        protected SharpDX.Direct3D11.Buffer IndexBuffer { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        protected SharpDX.Direct3D11.Buffer VertexBuffer { get; set; }

        /// <inheritdoc/>
        public void ApplyColor()
        {
            if (this.IsPrimitive)
            {
                this.VertexBuffer = SharpDX.Direct3D11.Buffer.Create(Engine.GameEngine.GraphicsManager.GetDevice, BindFlags.VertexBuffer, this.Vertexies);
            }
        }

        /// <summary>
        ///
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <inheritdoc/>
        public virtual void Draw()
        {
        }

        /// <inheritdoc/>
        public virtual void LoadContent(bool isNewObject = true)
        {
        }

        /// <inheritdoc/>
        public virtual void Render() { }

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
                this.IndexBuffer?.Dispose();
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
    }
}