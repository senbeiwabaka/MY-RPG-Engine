﻿namespace MY3DEngine.BaseObjects
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using MY3DEngine.GraphicObjects;
    using MY3DEngine.Interfaces;
    using Newtonsoft.Json;
    using SharpDX;
    using SharpDX.Direct3D11;

    /// <inheritdoc/>
    public abstract class BaseObject : IGameObject, INotifyPropertyChanged
    {
        private string name;
        private Vector3 position;

        #region Constructors

        /// <summary>
        /// blank constructor
        /// </summary>
        protected BaseObject()
            : this("Cube")
        { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">The name of the object</param>
        protected BaseObject(string name)
        {
            this.Name = name;
        }

        #endregion Constructors

        /// <inheritdoc/>
        public event PropertyChangedEventHandler PropertyChanged;

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

        /// <summary>
        /// The building blocks of the object
        /// </summary>
        public ColorVertex[] Vertexies { get; set; }

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
        public abstract void Draw();

        /// <inheritdoc/>
        public abstract void LoadContent(bool isNewObject = true);

        /// <inheritdoc/>
        public abstract void Render();

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
                this.VertexBuffer = null;

                this.IndexBuffer?.Dispose();
                this.IndexBuffer = null;
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
