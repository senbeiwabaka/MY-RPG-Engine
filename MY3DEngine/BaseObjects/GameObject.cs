using Newtonsoft.Json;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
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

        //[XmlIgnore]
        //public MeshClass MeshObject { get; protected set; }

        [JsonIgnore]
        public PixelShader PixelShader { get; set; }

        [JsonIgnore]
        public VertexShader VertextShader { get; set; }

        [JsonIgnore]
        protected virtual SharpDX.Direct3D11.Buffer Buffer { get; set; }

        [JsonIgnore]
        protected virtual InputLayout InputLayout { get; set; }

        //[JsonIgnore]
        public Vertex[] Vertex { get; set; }

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
            var path = System.IO.Path.GetFullPath("Shaders");

            // Compile Vertex shaders
            using (var vertexShaderByteCode = ShaderBytecode.CompileFromFile(string.Format("{0}\\Triangle.fx", path), "VS", "vs_4_0", ShaderFlags.EnableStrictness | ShaderFlags.Debug, EffectFlags.None))
            {
                this.VertextShader = new VertexShader(Engine.GameEngine.GraphicsManager.GetDevice, vertexShaderByteCode);

                this.InputLayout = new InputLayout(
                    Engine.GameEngine.GraphicsManager.GetDevice,
                    ShaderSignature.GetInputSignature(vertexShaderByteCode),
                    new[]
                    {
                        new InputElement("POSITION", 0, Format.R32G32B32A32_Float, 0, 0),
                        new InputElement("COLOR", 0, Format.R32G32B32A32_Float, 16, 0)
                    });
            }

            // Compile Pixel shaders
            using (var pixelShaderByteCode = ShaderBytecode.CompileFromFile(string.Format("{0}\\Triangle.fx", path), "PS", "ps_4_0", ShaderFlags.EnableStrictness | ShaderFlags.Debug, EffectFlags.None))
            {
                this.PixelShader = new PixelShader(Engine.GameEngine.GraphicsManager.GetDevice, pixelShaderByteCode);
            }
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
                this.Buffer?.Dispose();
                this.InputLayout?.Dispose();
                this.VertextShader?.Dispose();
                this.PixelShader?.Dispose();
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
                this.Buffer = SharpDX.Direct3D11.Buffer.Create(
                    Engine.GameEngine.GraphicsManager.GetDevice,
                    BindFlags.VertexBuffer,
                    this.Vertex);
            }
        }
    }
}