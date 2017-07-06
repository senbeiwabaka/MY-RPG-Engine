using System;

using SharpDX;

using Color = System.Drawing.Color;

namespace MY3DEngine.Graphics
{
    /// <summary>
    /// 
    /// </summary>
    public enum MeshType
    {
        Cube,
        Triangle,
        Cylinder,
        Cone,
        Teapot,
        Sphere,
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class MeshClass : IDisposable
    {
        private Material[] _material;
        private Matrix _world;

        public Texture[] CurrentTexture { get; set; }
        public Vector3 ObjectPosition { get; set; }
        public Vector3 ObjectRotate { get; set; }
        public bool IsShapeObject { get; private set; }
        public Vector3 ObjectScale { get; set; }
        public Mesh ObjectMesh { get; private set; }

        

        /// <summary>
        /// Return the mesh color as a string
        /// </summary>
        public string MeshColorasString
        {
            get
            {
                if (this._material != null)
                {
                    return this._material[0].Ambient.ToString();
                }

                return "No Color";
            }
        }

        /// <summary>
        /// Return the actual mesh color
        /// </summary>
        public Color MeshColor => this._material[0].Ambient.ToColor();

        /// <summary>
        /// Create a mesh from a .x File
        /// </summary>
        /// <param name="filePath">The path of the file</param>
        /// <param name="fileName">The name of the file</param>
        public MeshClass(string filePath, string fileName)
        {
            this.ObjectMesh = Mesh.FromFile(Engine.GameEngine.LocalDevice.GetDevice, filePath, MeshFlags.Managed);
            ExtendedMaterial[] externMaterial = this.ObjectMesh.GetMaterials();
            this._material = new Material[externMaterial.Length];
            this.CurrentTexture = new Texture[externMaterial.Length];

            for (int i = 0; i < externMaterial.Length; i++)
            {
                this._material[i] = externMaterial[i].MaterialD3D;
                this._material[i].Ambient = this._material[i].Diffuse;

                string s = filePath;
                int index = s.IndexOf(fileName);
                s = s.Remove(s.IndexOf(fileName));
                s = s.Insert(index, externMaterial[i].TextureFileName);

                this.CurrentTexture[i] = Texture.FromFile(Engine.GameEngine.LocalDevice.GetDevice, s);
            }

            //objectMesh.Optimize(MeshOptimizeFlags.Compact | MeshOptimizeFlags.AttributeSort);

            this.ObjectPosition = Vector3.Zero;
            this.ObjectRotate = Vector3.Zero;
            this.ObjectScale = new Vector3(1, 1, 1);
            this._world = Matrix.Identity;
            this.IsShapeObject = false;
        }

        /// <summary>
        /// For creating shape objects
        /// </summary>
        /// <param name="type">the name of the object you wish to create</param>
        public MeshClass(MeshType type = MeshType.Cube)
        {
            if (type == MeshType.Cube)
            {
                this.ObjectMesh = Mesh.CreateBox(Engine.GameEngine.LocalDevice.GetDevice, 1f, 1f, 1f);

                this.ObjectMesh.ComputeNormals();

                this.ObjectMesh.Optimize(MeshOptimizeFlags.Compact);

                this.ApplyColor(Color.White);
            }
            else if (type == MeshType.Sphere)
            {
                this.ObjectMesh = Mesh.CreateSphere(Engine.GameEngine.LocalDevice.GetDevice, .1f, 10, 10);

                this.ObjectMesh.ComputeNormals();
                this.ObjectMesh.Optimize(MeshOptimizeFlags.Compact);
                this.ApplyColor(Color.White);
            }
            else if (type == MeshType.Teapot)
            {
                this.ObjectMesh = Mesh.CreateTeapot(Engine.GameEngine.LocalDevice.GetDevice);

                this.ObjectMesh.ComputeNormals();

                this.ObjectMesh.OptimizeInPlace(MeshOptimizeFlags.Compact);

                this.ApplyColor(Color.White);
            }
            else if (type == MeshType.Triangle)
            {
                VertexPositionColor[] ShapeVertices = new VertexPositionColor[] {
                    new VertexPositionColor() { Color = Color.White.ToArgb(), Position = new Vector3(-1f, 0f, 1f) },
                    new VertexPositionColor() { Color = Color.White.ToArgb(), Position = new Vector3(1f, 0f, 1f) },
                    new VertexPositionColor() { Color = Color.White.ToArgb(), Position = new Vector3(-1f, 0f, -1f) },
                    new VertexPositionColor() { Color = Color.White.ToArgb(), Position = new Vector3(1f, 0f, -1f) },
                    new VertexPositionColor() { Color = Color.White.ToArgb(), Position = new Vector3(0f, 1f, 0f) },
                };

                var ShapeIndices = new short[] {
                    0, 2, 1,    // base
                    1, 2, 3,
                    0, 1, 4,    // sides
                    1, 3, 4,
                    3, 2, 4,
                    2, 0, 4,
                };

                this.ObjectMesh = new Mesh(Engine.GameEngine.LocalDevice.GetDevice, ShapeIndices.Length, ShapeVertices.Length, MeshFlags.Managed, VertexPositionColor.Format);

                this.ObjectMesh.LockVertexBuffer(LockFlags.None).WriteRange<VertexPositionColor>(ShapeVertices);
                this.ObjectMesh.UnlockVertexBuffer();

                this.ObjectMesh.LockIndexBuffer(LockFlags.None).WriteRange<short>(ShapeIndices);
                this.ObjectMesh.UnlockIndexBuffer();

                Mesh other = this.ObjectMesh.Clone(Engine.GameEngine.LocalDevice.GetDevice, MeshFlags.Managed, this.ObjectMesh.VertexFormat | VertexFormat.Normal | VertexFormat.Texture2);
                this.ObjectMesh.Dispose();
                this.ObjectMesh = null;
                //other.ComputeNormals();
                this.ObjectMesh = other.Clone(Engine.GameEngine.LocalDevice.GetDevice, MeshFlags.Managed, other.VertexFormat);
                other.Dispose();

                this.ObjectMesh.Optimize(MeshOptimizeFlags.Compact);

                this.ApplyColor(Color.White);
            }

            this.ObjectPosition = Vector3.Zero;
            this.ObjectRotate = Vector3.Zero;
            this.ObjectScale = new Vector3(1, 1, 1);
            this._world = Matrix.Translation(this.ObjectPosition);
            this.IsShapeObject = true;
        }

        #region releasing resources

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.CurrentTexture != null)
                {
                    foreach (var ct in this.CurrentTexture)
                    {
                        ct.Dispose();
                    }
                }

                this.ObjectMesh.Dispose();
            }

            this.CurrentTexture = null;
            this.ObjectMesh = null;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public void RenderMesh()
        {
            this._world = Matrix.RotationYawPitchRoll(this.ObjectRotate.Y, this.ObjectRotate.X, this.ObjectRotate.Z) * 
                Matrix.Scaling(this.ObjectScale) * Matrix.Translation(this.ObjectPosition);
            Engine.GameEngine.LocalDevice.GetDevice.SetTransform(TransformState.World, this._world);

            if (this._material != null)
            {
                foreach (var item in this._material)
                {
                    Engine.GameEngine.LocalDevice.GetDevice.Material = item;
                }
            }

            if (this.CurrentTexture != null)
            {
                foreach (var item in this.CurrentTexture)
                {
                    Engine.GameEngine.LocalDevice.GetDevice.SetTexture(0, item);
                }
            }

            if (this.ObjectMesh != null)
            {
                this.ObjectMesh.DrawSubset(0);
            }
        }

        /// <summary>
        /// Apply a color to a mesh
        /// </summary>
        /// <param name="ambientColor">The color you wish you apply</param>
        /// <param name="diffuseColor">Have to make it nullable because you can't make a compile-time constant color</param>
        public void ApplyColor(Color ambientColor, Color? diffuseColor = null)
        {
            this._material = new Material[1];
            this._material[0].Ambient = ambientColor;
            this._material[0].Diffuse = ambientColor;
            this._material[0].Specular = ambientColor;
            this._material[0].Emissive = Color.Black;
            this._material[0].Power = 50.0f;
        }

        /// <summary>
        /// To apply a user supplied texture to the object
        /// </summary>
        /// <param name="filePath">The string path of the file</param>
        /// <param name="fileName">The string name of the file</param>
        public void ApplyTextureMesh(string filePath, string fileName)
        {
            lock (this.ObjectMesh)
            {
                Mesh other = this.ObjectMesh.Clone(Engine.GameEngine.LocalDevice.GetDevice, MeshFlags.UseHardwareOnly,
                    VertexFormat.Position | VertexFormat.Normal | VertexFormat.Texture0);
                lock (this.CurrentTexture)
                {
                }

                other.Dispose();
            }
        }

        /// <summary>
        /// Rotate the object
        /// </summary>
        /// <param name="x">x units to change</param>
        /// <param name="y">y units to change</param>
        /// <param name="z">z units to change</param>
        public void Rotate(float x, float y, float z)
        {
            this.ObjectRotate = new Vector3(x, y, z);
        }

        /// <summary>
        /// Translate the object
        /// </summary>
        /// <param name="x">x units to change</param>
        /// <param name="y">y units to change</param>
        /// <param name="z">z units to change</param>
        public void Translate(float x = 0, float y = 0, float z = 0)
        {
            this.ObjectPosition = new Vector3(x, y, z);
        }

        /// <summary>
        /// Scale the object
        /// </summary>
        /// <param name="scale">vector 3 to change the scale</param>
        public void Scale(Vector3 scale)
        {
            this.ObjectScale = scale;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public struct VertexPositionColor
    {
        public Vector3 Position { get; set; }
        public int Color { get; set; }

        public static VertexFormat Format = VertexFormat.Position | VertexFormat.Diffuse;
        public static int VertexByteSize = 16;

        public static VertexDeclaration VertexDecl = new VertexDeclaration(Engine.GameEngine.LocalDevice.GetDevice, new VertexElement[]
            {
                new VertexElement(0, 0, DeclarationType.Float3, DeclarationMethod.Default, DeclarationUsage.Position, 0),
                new VertexElement(0, 12, DeclarationType.Color, DeclarationMethod.Default, DeclarationUsage.Color, 0),
                VertexElement.VertexDeclarationEnd
            });
    }
}