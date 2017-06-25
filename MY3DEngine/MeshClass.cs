using SlimDX;
using SlimDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace MY3DEngine
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
        public Texture[] CurrentTexture { get; set; }
        public Vector3 ObjectPosition { get; set; }
        public Vector3 ObjectRotate { get; set; }
        public bool IsShapeObject { get; private set; }
        public Vector3 ObjectScale { get; set; }
        public Mesh ObjectMesh { get; private set; }

        private Material[] _material;
        private Matrix _world;

        /// <summary>
        /// Return the mesh color as a string
        /// </summary>
        public string MeshColorasString
        {
            get
            {
                if (_material != null)
                {
                    return _material[0].Ambient.ToString();
                }

                return "No Color";
            }
        }

        /// <summary>
        /// Return the actual mesh color
        /// </summary>
        public Color MeshColor
        {
            get
            {
                return _material[0].Ambient.ToColor();
            }
        }

        /// <summary>
        /// Create a mesh from a .x File
        /// </summary>
        /// <param name="filePath">The path of the file</param>
        /// <param name="fileName">The name of the file</param>
        public MeshClass(string filePath, string fileName)
        {
            ObjectMesh = Mesh.FromFile(Engine.GameEngine.LocalDevice.Device, filePath, MeshFlags.Managed);
            ExtendedMaterial[] externMaterial = ObjectMesh.GetMaterials();
            _material = new Material[externMaterial.Length];
            CurrentTexture = new Texture[externMaterial.Length];

            for (int i = 0; i < externMaterial.Length; i++)
            {
                _material[i] = externMaterial[i].MaterialD3D;
                _material[i].Ambient = _material[i].Diffuse;

                string s = filePath;
                int index = s.IndexOf(fileName);
                s = s.Remove(s.IndexOf(fileName));
                s = s.Insert(index, externMaterial[i].TextureFileName);

                CurrentTexture[i] = Texture.FromFile(Engine.GameEngine.LocalDevice.Device, s);
            }

            //objectMesh.Optimize(MeshOptimizeFlags.Compact | MeshOptimizeFlags.AttributeSort);

            ObjectPosition = Vector3.Zero;
            ObjectRotate = Vector3.Zero;
            ObjectScale = new Vector3(1, 1, 1);
            _world = Matrix.Identity;
            IsShapeObject = false;
        }

        /// <summary>
        /// For creating shape objects
        /// </summary>
        /// <param name="type">the name of the object you wish to create</param>
        public MeshClass(MeshType type = MeshType.Cube)
        {
            if (type == MeshType.Cube)
            {
                ObjectMesh = Mesh.CreateBox(Engine.GameEngine.LocalDevice.Device, 1f, 1f, 1f);

                ObjectMesh.ComputeNormals();

                ObjectMesh.Optimize(MeshOptimizeFlags.Compact);

                ApplyColor(Color.White);
            }
            else if (type == MeshType.Sphere)
            {
                ObjectMesh = Mesh.CreateSphere(Engine.GameEngine.LocalDevice.Device, .1f, 10, 10);

                ObjectMesh.ComputeNormals();
                ObjectMesh.Optimize(MeshOptimizeFlags.Compact);
                ApplyColor(Color.White);
            }
            else if (type == MeshType.Teapot)
            {
                ObjectMesh = Mesh.CreateTeapot(Engine.GameEngine.LocalDevice.Device);

                ObjectMesh.ComputeNormals();

                ObjectMesh.OptimizeInPlace(MeshOptimizeFlags.Compact);

                ApplyColor(Color.White);
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

                ObjectMesh = new Mesh(Engine.GameEngine.LocalDevice.Device, ShapeIndices.Length, ShapeVertices.Length, MeshFlags.Managed, VertexPositionColor.Format);

                ObjectMesh.LockVertexBuffer(LockFlags.None).WriteRange<VertexPositionColor>(ShapeVertices);
                ObjectMesh.UnlockVertexBuffer();

                ObjectMesh.LockIndexBuffer(LockFlags.None).WriteRange<short>(ShapeIndices);
                ObjectMesh.UnlockIndexBuffer();

                Mesh other = ObjectMesh.Clone(Engine.GameEngine.LocalDevice.Device, MeshFlags.Managed, ObjectMesh.VertexFormat | VertexFormat.Normal | VertexFormat.Texture2);
                ObjectMesh.Dispose();
                ObjectMesh = null;
                //other.ComputeNormals();
                ObjectMesh = other.Clone(Engine.GameEngine.LocalDevice.Device, MeshFlags.Managed, other.VertexFormat);
                other.Dispose();

                ObjectMesh.Optimize(MeshOptimizeFlags.Compact);

                ApplyColor(Color.White);
            }

            ObjectPosition = Vector3.Zero;
            ObjectRotate = Vector3.Zero;
            ObjectScale = new Vector3(1, 1, 1);
            _world = Matrix.Translation(ObjectPosition);
            IsShapeObject = true;
        }

        #region releasing resources

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (CurrentTexture != null)
                {
                    foreach (var ct in CurrentTexture)
                    {
                        ct.Dispose();
                    }
                }

                ObjectMesh.Dispose();
            }

            CurrentTexture = null;
            ObjectMesh = null;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public void RenderMesh()
        {
            _world = Matrix.RotationYawPitchRoll(ObjectRotate.Y, ObjectRotate.X, ObjectRotate.Z) * 
                Matrix.Scaling(ObjectScale) * Matrix.Translation(ObjectPosition);
            Engine.GameEngine.LocalDevice.Device.SetTransform(TransformState.World, _world);

            if (_material != null)
            {
                foreach (var item in _material)
                {
                    Engine.GameEngine.LocalDevice.Device.Material = item;
                }
            }

            if (CurrentTexture != null)
            {
                foreach (var item in CurrentTexture)
                {
                    Engine.GameEngine.LocalDevice.Device.SetTexture(0, item);
                }
            }

            if (ObjectMesh != null)
            {
                ObjectMesh.DrawSubset(0);
            }
        }

        /// <summary>
        /// Apply a color to a mesh
        /// </summary>
        /// <param name="ambientColor">The color you wish you apply</param>
        /// <param name="diffuseColor">Have to make it nullable because you can't make a compile-time constant color</param>
        public void ApplyColor(Color ambientColor, Color? diffuseColor = null)
        {
            _material = new Material[1];
            _material[0].Ambient = ambientColor;
            _material[0].Diffuse = ambientColor;
            _material[0].Specular = ambientColor;
            _material[0].Emissive = Color.Black;
            _material[0].Power = 50.0f;
        }

        /// <summary>
        /// To apply a user supplied texture to the object
        /// </summary>
        /// <param name="filePath">The string path of the file</param>
        /// <param name="fileName">The string name of the file</param>
        public void ApplyTextureMesh(string filePath, string fileName)
        {
            lock (ObjectMesh)
            {
                Mesh other = ObjectMesh.Clone(Engine.GameEngine.LocalDevice.Device, MeshFlags.UseHardwareOnly,
                    VertexFormat.Position | VertexFormat.Normal | VertexFormat.Texture0);
                lock (CurrentTexture)
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
            ObjectRotate = new Vector3(x, y, z);
        }

        /// <summary>
        /// Translate the object
        /// </summary>
        /// <param name="x">x units to change</param>
        /// <param name="y">y units to change</param>
        /// <param name="z">z units to change</param>
        public void Translate(float x = 0, float y = 0, float z = 0)
        {
            ObjectPosition = new Vector3(x, y, z);
        }

        /// <summary>
        /// Scale the object
        /// </summary>
        /// <param name="scale">vector 3 to change the scale</param>
        public void Scale(Vector3 scale)
        {
            ObjectScale = scale;
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

        public static VertexDeclaration VertexDecl = new VertexDeclaration(Engine.GameEngine.LocalDevice.Device, new VertexElement[]
            {
                new VertexElement(0, 0, DeclarationType.Float3, DeclarationMethod.Default, DeclarationUsage.Position, 0),
                new VertexElement(0, 12, DeclarationType.Color, DeclarationMethod.Default, DeclarationUsage.Color, 0),
                VertexElement.VertexDeclarationEnd
            });
    }
}