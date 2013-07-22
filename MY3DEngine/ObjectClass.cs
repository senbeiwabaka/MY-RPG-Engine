using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MY3DEngine
{
    [Serializable]
    public class ObjectClass : IDisposable
    {
        public string FileName { get; private set; }

        public string FilePath { get; private set; }

        [XmlIgnore]
        public MeshClass MeshObject { get; protected set; }

        public string Name { get; set; }

        #region Constructors

        public ObjectClass()
        {
        }

        public ObjectClass(string name = "", string fileName = "", string path = "")
        {
            MeshObject = new MeshClass(path, fileName);
        }

        #endregion Constructors

        public virtual void Dispose()
        {
            MeshObject.Dispose();
        }

        public virtual void Renderer()
        {
            MeshObject.RenderMesh();
        }
    }
}