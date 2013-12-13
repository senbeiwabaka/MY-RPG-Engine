using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MY3DEngine
{
    [Serializable]
    public class GameObject : IDisposable
    {
        public string FileName { get; private set; }

        public string FilePath { get; private set; }

        public int ID { get; set; }

        [XmlIgnore]
        public MeshClass MeshObject { get; protected set; }

        public string Name { get; set; }

        #region Constructors

        /// <summary>
        /// blank constructor
        /// </summary>
        public GameObject()
        {
        }

        /// <summary>
        /// Constructor for building a .x object
        /// </summary>
        /// <param name="fileName">The file name of the object</param>
        /// <param name="path">The path of the object</param>
        public GameObject(string fileName = "", string path = "")
        {
            MeshObject = new MeshClass(path, fileName);
            FileName = fileName;
            FilePath = path;
        }

        public GameObject(string type = "Cube")
        {
            if (type.ToLower().Equals("triangle"))
            {
                MeshObject = new MeshClass(MeshType.Triangle);
            }
            else
            {
                MeshObject = new MeshClass(MeshType.Cube);
            }

            Name = type;
        }

        #endregion Constructors

        /// <summary>
        /// Disposes of the object's mesh
        /// </summary>
        public virtual void Dispose()
        {
            MeshObject.Dispose();
        }

        public virtual void Renderer()
        {
            if (MeshObject != null)
            {
                MeshObject.RenderMesh();
            }
        }

        public override string ToString()
        {
            return ID + ". " + Name;
        }
    }
}