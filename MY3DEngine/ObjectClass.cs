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
        public string Name { get; set; }
        public string FileName { get; private set; }
        public string FilePath { get; private set; }
        [XmlIgnore]
        public MeshClass MeshObject { get; protected set; }

        #region Constructors

        public ObjectClass()
        {
        }

        public ObjectClass(string name = "", string fileName = "", string path = "")
        {
        }

        #endregion

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
